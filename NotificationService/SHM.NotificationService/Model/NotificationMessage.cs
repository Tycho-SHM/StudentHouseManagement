namespace SHM.NotificationService.Model;

public struct NotificationMessage
{
    public NotificationType NotificationType { get; set; }
    public string Message { get; set; }
    public string RecipientUserId { get; set; }
}