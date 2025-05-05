using Microsoft.Extensions.Logging;
using SHM.ProfileService.Abstractions.Business;
using SHM.ProfileService.Abstractions.Repositories;
using SHM.ProfileService.Model;

namespace SHM.ProfileService;

public class UserProfileBusiness : IUserProfileBusiness
{
    private readonly ILogger<UserProfileBusiness> _logger;
    private readonly IUserProfileRepository _userProfileRepository;

    public UserProfileBusiness(ILogger<UserProfileBusiness> logger, IUserProfileRepository userProfileRepository)
    {
        _logger = logger;
        _userProfileRepository = userProfileRepository;
    }
    
    public async Task<UserProfile> GetOrCreate(string userId)
    {
        var userProfile = await _userProfileRepository.GetByUserId(userId);
        if (userProfile != null)
        {
            return userProfile; 
        }

        userProfile = new UserProfile
        {
            UserId = userId
        };

        return await _userProfileRepository.Add(userProfile);
    }

    public async Task<UserProfile?> GetById(Guid id)
    {
        return await _userProfileRepository.GetById(id);
    }

    public async Task<UserProfile?> Update(UserProfile userProfile)
    {
        var oldUserProfile = await GetById(userProfile.Id);
        if (oldUserProfile == null)
        {
            return null;
        }

        if (userProfile.DisplayName != null)
        {
            oldUserProfile.DisplayName = userProfile.DisplayName;
        }

        if (userProfile.ImgUrl != null)
        {
            oldUserProfile.ImgUrl = userProfile.ImgUrl;
        }
        
        oldUserProfile.LastUpdatedDateTimeUtc = DateTime.UtcNow;
        return await _userProfileRepository.Update(oldUserProfile);
    }

    public async Task<bool> Delete(string userId)
    {
        var userProfile = await _userProfileRepository.GetByUserId(userId);
        if (userProfile == null)
        {
            return false;
        }

        userProfile.DisplayName = "Deleted Profile";
        userProfile.ImgUrl = null;
        userProfile.LastUpdatedDateTimeUtc = DateTime.UtcNow;
        
        userProfile.Deleted = true;

        var updatedUserProfile = await _userProfileRepository.Update(userProfile);

        return updatedUserProfile.Deleted;
    }
}