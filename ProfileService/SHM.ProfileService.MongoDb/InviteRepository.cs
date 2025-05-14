using SHM.ProfileService.Abstractions.Repositories;
using SHM.ProfileService.Model;

namespace SHM.ProfileService.MongoDb;

public class InviteRepository : IInviteRepository
{
    public async Task<List<Invite>> GetInvitesByInvitedByUserProfileId(Guid userProfileId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Invite>> GetInvitesByInvitedUserProfileId(Guid userProfileId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Invite>> GetInvitesByHouseProfileId(Guid houseProfileId)
    {
        throw new NotImplementedException();
    }

    public async Task<Invite> Add(Invite invite)
    {
        throw new NotImplementedException();
    }

    public async Task<Invite> Update(Invite invite)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}