using Microsoft.Extensions.Logging;
using SHM.ProfileService.Abstractions.Business;
using SHM.ProfileService.Abstractions.Repositories;
using SHM.ProfileService.Model;
using SHM.ProfileService.Model.Invite;

namespace SHM.ProfileService;

public class InviteBusiness : IInviteBusiness
{
    private readonly ILogger<InviteBusiness> _logger;
    private readonly IInviteRepository _inviteRepository;

    public InviteBusiness(ILogger<InviteBusiness> logger, IInviteRepository inviteRepository)
    {
        _logger = logger;
        _inviteRepository = inviteRepository;
    }
    
    public async Task<List<Invite>> GetReceivedInvites(Guid userProfileId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Invite>> GetSentInvites(Guid userProfileId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Invite>> GetHouseInvites(Guid houseProfileId)
    {
        throw new NotImplementedException();
    }
}