using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SHM.ProfileService.Model.House;

public class HouseMembership
{
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid Id { get; set; } = Guid.NewGuid();
    public required UserProfile UserProfile { get; set; }
    public DateTime CreatedDateTimeUtc { get; set; } = DateTime.UtcNow;
}