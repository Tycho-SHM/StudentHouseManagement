using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SHM.ProfileService.Abstractions.Business;
using SHM.ProfileService.Model.Invite;

namespace SHM.ProfileService.API.Controllers;

[ApiController]
[Route("[controller]")]
public class InviteController : ControllerBase
{
    private readonly IInviteBusiness _inviteBusiness;
    private readonly IUserProfileBusiness _userProfileBusiness;
    private readonly ILogger<InviteController> _logger;

    public InviteController(ILogger<InviteController> logger, IInviteBusiness inviteBusiness, IUserProfileBusiness userProfileBusiness)
    {
        _logger = logger;
        _inviteBusiness = inviteBusiness;
        _userProfileBusiness = userProfileBusiness;
    }

    [HttpGet("Received/{userProfileId:guid}")]
    [Authorize]
    public async Task<ActionResult<List<Invite>>> GetReceived(Guid userProfileId)
    {
        var user = HttpContext.User;
        if (!user.HasClaim(claim => claim.Type.Equals("sub"))) return BadRequest();

        if (!userProfileId.Equals(Guid.Parse(user.FindFirst("sub")!.Value))) return Unauthorized();

        return await _inviteBusiness.GetReceivedInvites(userProfileId);
    }

    [HttpGet("Sent/{userProfileId:guid}")]
    [Authorize]
    public async Task<ActionResult<List<Invite>>> GetSent(Guid userProfileId)
    {
        var user = HttpContext.User;
        if (!user.HasClaim(claim => claim.Type.Equals("sub"))) return BadRequest();

        if (!userProfileId.Equals(Guid.Parse(user.FindFirst("sub")!.Value))) return Unauthorized();

        return await _inviteBusiness.GetSentInvites(userProfileId);
    }
    
    [HttpPost("Invite")]
    [Authorize]
    public async Task<ActionResult<Invite>> Invite(Guid invitedUserProfileId, Guid houseProfileId)
    {
        var user = HttpContext.User;
        if (!user.HasClaim(claim => claim.Type.Equals("sub"))) return BadRequest();

        var userProfile = await _userProfileBusiness.GetOrCreate(user.FindFirst("sub")!.Value);
        
        return await _inviteBusiness.SendInviteByUserProfileIdToHouse(userProfile.Id, invitedUserProfileId, houseProfileId);
    }
}