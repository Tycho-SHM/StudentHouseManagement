namespace SHM.ProfileService.Model.House;

public class HouseProfile
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public required string Name { get; set; }
    public string? ImgUrl { get; set; }
    public DateTime CreatedDateTimeUtc { get; set; } = DateTime.UtcNow;
    public List<HouseMembership> Members { get; set; }
}