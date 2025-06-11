namespace SHM.ProfileService.Model.Messaging;

public struct NotificationMessage
{
    public NotificationType NotificationType { get; set; }
    public string Message { get; set; }
    public string RecipientUserId { get; set; }
}