using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SHM.ProfileService.Model;

public class Invite
{
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid Id { get; set; } = Guid.NewGuid();
    public InviteStatus Status { get; set; }
    public required UserProfile InvitedBy { get; set; }
    public required UserProfile InvitedUser { get; set; }
    public required HouseProfile House { get; set; }
    public DateTime CreatedDateTimeUtc { get; set; } = DateTime.UtcNow;
}