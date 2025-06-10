using Microsoft.Extensions.Logging;
using SHM.ProfileService.Abstractions.Repositories;
using SHM.ProfileService.Model.Invite;

namespace SHM.ProfileService.EfCore.Repository;

public class InviteRepository : IInviteRepository
{
    private readonly ProfileContext _dbContext;
    private readonly ILogger<InviteRepository> _logger;

    public InviteRepository(ILogger<InviteRepository> logger, ProfileContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }
    
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
        await _dbContext.Invites.AddAsync(invite);
        await _dbContext.SaveChangesAsync();
        return invite;
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