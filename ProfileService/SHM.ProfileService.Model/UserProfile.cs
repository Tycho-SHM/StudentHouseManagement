using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SHM.ProfileService.Model;

public class UserProfile
{
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string DisplayName { get; set; }
    public string UserId { get; set; }
    public List<HouseProfile> Houses { get; set; }
}