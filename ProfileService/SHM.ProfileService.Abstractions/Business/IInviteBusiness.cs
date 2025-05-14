using SHM.ProfileService.Model;

namespace SHM.ProfileService.Abstractions.Business;

public interface IInviteBusiness
{
    public Task<List<Invite>> GetReceivedInvites(Guid userProfileId);
    public Task<List<Invite>> GetSentInvites(Guid userProfileId);
    public Task<List<Invite>> GetHouseInvites(Guid houseProfileId);
}