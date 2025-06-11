using System.Text.Json;
using Clerk.BackendAPI;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MimeKit;
using SHM.MessageQueues.Abstractions;
using SHM.NotificationService.Model;

namespace SHM.NotificationService;

public class NotificationService : IHostedService
{
    private readonly IMessageBrokerConnection _messageBrokerConnection;
    private readonly ClerkBackendApi _clerkBackendApi;
    private readonly NotificationServiceOptions _options;
    
    public NotificationService(IMessageBrokerConnection messageBrokerConnection, IOptions<NotificationServiceOptions> options)
    {
        _messageBrokerConnection = messageBrokerConnection;
        
        _options = options.Value;
        _clerkBackendApi = new ClerkBackendApi(_options.ClerkApiSecret);
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _messageBrokerConnection.SubscribeToQueue("notifications", HandleMessage);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Shutting down notificationservice");
    }

    private async Task HandleMessage(string message)
    {
        Console.WriteLine("NEW MESSAGE FROM MESSAGEBUS: " + message);

        var notificationToSend = JsonSerializer.Deserialize<NotificationMessage>(message);
        
        var recipient = await _clerkBackendApi.Users.GetAsync(notificationToSend.RecipientUserId);
        var recipientEmail = recipient.User.EmailAddresses.FirstOrDefault().EmailAddressValue;
        
        var mailMessage = new MimeMessage();
        mailMessage.From.Add(new MailboxAddress("noreply", "noreply@shm.tycho.dev"));
        mailMessage.To.Add(new MailboxAddress(recipientEmail, recipientEmail));
        mailMessage.Subject = notificationToSend.NotificationType.ToString();
        mailMessage.Body = new TextPart("html")
        {
            Text = notificationToSend.Message
        };
        var client = new SmtpClient();
        try
        {
            client.SslProtocols = System.Security.Authentication.SslProtocols.Tls12;
            client.Connect("smtp.zeptomail.eu", 587, false);
            client.Authenticate("emailapikey", _options.ZeptoMailKey);
            client.Send(mailMessage);
            client.Disconnect(true);
        }
        catch (Exception e)
        {
            Console.Write(e.Message);
        }
    }
}