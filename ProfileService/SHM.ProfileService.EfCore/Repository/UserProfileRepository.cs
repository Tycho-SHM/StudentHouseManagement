using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SHM.ProfileService.Abstractions.Repositories;
using SHM.ProfileService.Model;

namespace SHM.ProfileService.EfCore.Repository;

public class UserProfileRepository : IUserProfileRepository
{
    private readonly ProfileContext _dbContext;
    private readonly ILogger<UserProfileRepository> _logger;

    public UserProfileRepository(ILogger<UserProfileRepository> logger, ProfileContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task<List<UserProfile>> GetAll()
    {
        return await _dbContext.UserProfiles.ToListAsync();
    }

    public async Task<UserProfile?> GetById(Guid id)
    {
        return await _dbContext.UserProfiles.FindAsync(id);
    }

    public async Task<UserProfile?> GetByUserId(string userId)
    {
        return await _dbContext.UserProfiles.FirstOrDefaultAsync(x => x.UserId.Equals(userId));
    }

    public async Task<UserProfile> Add(UserProfile userProfile)
    {
        await _dbContext.UserProfiles.AddAsync(userProfile);
        await _dbContext.SaveChangesAsync();
        return userProfile;
    }

    public async Task<UserProfile> Update(UserProfile userProfile)
    {
        userProfile.LastUpdatedDateTimeUtc = DateTime.UtcNow;
        _dbContext.UserProfiles.Update(userProfile);
        await _dbContext.SaveChangesAsync();
        return userProfile;
    }
}