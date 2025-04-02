using SHM.ProfileService.Model;

namespace SHM.ProfileService.Abstractions.Repositories;

public interface IUserProfileRepository
{
    public Task<List<UserProfile>> GetAll();
    public Task<UserProfile?> GetById(Guid id);
    public Task<UserProfile?> GetByUserId(string userId);
    public Task<UserProfile> Add(UserProfile userProfile);
    public Task<UserProfile> Update(UserProfile userProfile);
}