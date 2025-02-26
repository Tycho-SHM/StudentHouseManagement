using System.Collections.Concurrent;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SHM.MessageQueues.Abstractions;

namespace SHM.MessageQueues.RabbitMQ;

public class RabbitMQConnection : IMessageBrokerConnection
{
    private readonly ConnectionFactory _factory;
    
    private readonly SemaphoreSlim _connectionSemaphore = new(1, 1);
    private IConnection? _connection;

    private readonly SemaphoreSlim _channelSemaphore = new(1, 1);
    private readonly ConcurrentDictionary<string, IChannel?> _channels = new();
    
    public RabbitMQConnection()
    {
        _factory = new ConnectionFactory
        {
            HostName = "localhost",
            UserName = "admin",
            Password = "Tycho123!",
        };
    }

    private async Task<IConnection> GetConnection()
    {
        if (_connection != null && _connection.IsOpen)
        {
            return _connection;
        }
        
        await _connectionSemaphore.WaitAsync();
        try
        {
            if (_connection == null || !_connection.IsOpen)
            {
                _connection = await _factory.CreateConnectionAsync();
            }
            
            return _connection;
        }
        finally
        {
            _connectionSemaphore.Release();
        }
    }

    private async Task<IChannel> GetChannel(string queueName)
    {
        if (_channels.TryGetValue(queueName, out var existingChannel) && existingChannel!.IsOpen)
        {
            return existingChannel;
        }
        
        await _channelSemaphore.WaitAsync();
        try
        {
            if (!_channels.TryGetValue(queueName, out var channel) || !channel!.IsOpen)
            {
                var connection = await GetConnection();
                channel = await connection.CreateChannelAsync();
                _channels[queueName] = channel;

                await _channels[queueName]!.QueueDeclareAsync(queueName, true, false, false, null);
            }
            
            return channel!;
        }
        finally
        {
            _channelSemaphore.Release();
        }
    }
    
    public async Task PublishMessageToQueue(string queueName, string message)
    {
        var channel = await GetChannel(queueName);
        
        await channel.BasicPublishAsync(string.Empty, queueName, Encoding.UTF8.GetBytes(message));
    }

    public async Task SubscribeToQueue(string queueName, Func<string, Task> messageHandler)
    {
        var channel = await GetChannel(queueName);
        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            await messageHandler(message);
        };
        
        await channel.BasicConsumeAsync(queueName, true, consumer);
    }
}