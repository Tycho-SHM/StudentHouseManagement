using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SHM.ProfileService.Model;

public class UserProfile
{
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? ImgUrl { get; set; }
    public string? UserId { get; set; }
    public DateTime LastUpdatedDateTimeUtc { get; set; } = DateTime.UtcNow;
    public DateTime CreatedDateTimeUtc { get; set; } = DateTime.UtcNow;
    public string? DisplayName { get; set; }
    public List<HouseProfile> Houses { get; set; } = new List<HouseProfile>();
}