namespace SHM.MessageQueues.Abstractions;

public interface IMessageBrokerConnection
{
    public Task PublishMessageToQueue(string queueName, string message);
    public Task SubscribeToQueue(string queueName, Func<string, Task> messageHandler);
}