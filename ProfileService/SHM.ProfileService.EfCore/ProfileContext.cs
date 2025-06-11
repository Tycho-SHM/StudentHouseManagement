using Microsoft.EntityFrameworkCore;
using SHM.ProfileService.Model;
using SHM.ProfileService.Model.House;
using SHM.ProfileService.Model.Invite;

namespace SHM.ProfileService.EfCore;

public sealed class ProfileContext : DbContext
{
    public ProfileContext(DbContextOptions<ProfileContext> options) : base(options)
    {
        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Testing")
        {
            Database.EnsureCreated();
        }
    }

    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<HouseProfile> HouseProfiles { get; set; }
    public DbSet<HouseMembership> HouseMemberships { get; set; }
    public DbSet<Invite> Invites { get; set; }
}