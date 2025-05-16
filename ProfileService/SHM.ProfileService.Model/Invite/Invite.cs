using SHM.ProfileService.Model.House;

namespace SHM.ProfileService.Model.Invite;

public class Invite
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required UserProfile InvitedByUserProfile { get; set; }
    public required UserProfile InvitedUserProfile { get; set; }
    public required HouseProfile InvitedTo { get; set; }
    public DateTime CreatedDateTimeUtc { get; set; } = DateTime.UtcNow;
}