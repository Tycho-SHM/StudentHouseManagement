using SHM.ProfileService.Model;

namespace SHM.ProfileService.Abstractions.Business;

public interface IUserProfileBusiness
{
    public Task<UserProfile> GetOrCreate(string userId);
    public Task<UserProfile?> GetById(Guid id);
    public Task<UserProfile> Update(UserProfile userProfile);
}