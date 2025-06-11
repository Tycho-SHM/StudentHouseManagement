namespace SHM.ProfileService.Model.House;

public class HouseMembership
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required UserProfile UserProfile { get; set; }
    public DateTime CreatedDateTimeUtc { get; set; } = DateTime.UtcNow;
}