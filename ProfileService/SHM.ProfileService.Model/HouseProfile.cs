using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SHM.ProfileService.Model;

public class HouseProfile
{
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid Id { get; } = Guid.NewGuid();
    
    public required string Name { get; set; }
    
    public string? ImgUrl { get; set; }
    
    public DateTime CreatedDateTimeUtc { get; } = DateTime.UtcNow;
}