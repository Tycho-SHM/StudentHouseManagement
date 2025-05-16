using Microsoft.EntityFrameworkCore;
using SHM.ProfileService.Model;
using SHM.ProfileService.Model.House;
using SHM.ProfileService.Model.Invite;

namespace SHM.ProfileService.EfCore;

public class ProfileContext : DbContext
{
    public ProfileContext(DbContextOptions<ProfileContext> options) : base(options)
    {
    }

    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<HouseProfile> HouseProfiles { get; set; }
    public DbSet<HouseMembership> HouseMemberships { get; set; }
    public DbSet<Invite> Invites { get; set; }
}