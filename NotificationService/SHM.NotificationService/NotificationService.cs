using Microsoft.Extensions.Hosting;
using SHM.MessageQueues.Abstractions;

namespace SHM.NotificationService;

public class NotificationService : IHostedService
{
    private readonly IMessageBrokerConnection _messageBrokerConnection;
    
    public NotificationService(IMessageBrokerConnection messageBrokerConnection)
    {
        _messageBrokerConnection = messageBrokerConnection;
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _messageBrokerConnection.SubscribeToQueue("notifications", HandleMessage);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Shutting down notificationservice");
    }

    private Task HandleMessage(string message)
    {
        Console.WriteLine("NEW MESSAGE FROM MESSAGEBUS: " + message);
        return Task.CompletedTask;
    }
}