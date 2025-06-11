using System.Text.Json;
using Microsoft.Extensions.Logging;
using SHM.MessageQueues.Abstractions;
using SHM.ProfileService.Abstractions.Business;
using SHM.ProfileService.Abstractions.Repositories;
using SHM.ProfileService.Model.Invite;
using SHM.ProfileService.Model.Messaging;

namespace SHM.ProfileService;

public class InviteBusiness : IInviteBusiness
{
    private readonly IInviteRepository _inviteRepository;
    private readonly IHouseProfileRepository _houseProfileRepository;
    private readonly IUserProfileRepository _userProfileRepository;
    private readonly IMessageBrokerConnection _messageBrokerConnection;
    private readonly ILogger<InviteBusiness> _logger;

    public InviteBusiness(ILogger<InviteBusiness> logger, IInviteRepository inviteRepository, IHouseProfileRepository houseProfileRepository, IUserProfileRepository userProfileRepository, IMessageBrokerConnection messageBrokerConnection)
    {
        _logger = logger;
        _inviteRepository = inviteRepository;
        _houseProfileRepository = houseProfileRepository;
        _userProfileRepository = userProfileRepository;
        _messageBrokerConnection = messageBrokerConnection;
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

    public async Task<Invite> SendInviteByUserProfileIdToHouse(Guid userProfileId, Guid invitedUserProfileId, Guid houseProfileId)
    {
        var houseProfile = await _houseProfileRepository.GetById(houseProfileId);
        if (houseProfile == null) return null;

        var invitedUserProfile = await _userProfileRepository.GetById(invitedUserProfileId);
        if(invitedUserProfile == null) return null;
        
        var userProfile = await _userProfileRepository.GetById(userProfileId);
        if(userProfile == null) return null;
        
        var invite = new Invite
        {
            InvitedByUserProfile = userProfile,
            InvitedUserProfile = invitedUserProfile,
            InvitedTo = houseProfile,
            CreatedDateTimeUtc = DateTime.UtcNow
        };
        
        invite = await _inviteRepository.Add(invite);

        var message = new NotificationMessage()
        {
            NotificationType = NotificationType.Invite,
            Message = $"You have been invited to join {houseProfile.Name} by {userProfile.DisplayName}",
            RecipientUserId = invitedUserProfile.UserId
        };
        
        await _messageBrokerConnection.PublishMessageToQueue("notifications", JsonSerializer.Serialize(message));
        
        return invite;
    }
}