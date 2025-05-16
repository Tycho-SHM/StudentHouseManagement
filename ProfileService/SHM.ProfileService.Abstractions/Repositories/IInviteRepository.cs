using SHM.ProfileService.Model.Invite;

namespace SHM.ProfileService.Abstractions.Repositories;

public interface IInviteRepository
{
    public Task<List<Invite>> GetInvitesByInvitedByUserProfileId(Guid userProfileId);
    public Task<List<Invite>> GetInvitesByInvitedUserProfileId(Guid userProfileId);
    public Task<List<Invite>> GetInvitesByHouseProfileId(Guid houseProfileId);
    public Task<Invite> Add(Invite invite);
    public Task<Invite> Update(Invite invite);
    public Task<bool> Delete(Guid id);
}