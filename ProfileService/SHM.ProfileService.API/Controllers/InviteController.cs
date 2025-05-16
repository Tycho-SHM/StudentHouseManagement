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
    private readonly ILogger<InviteController> _logger;

    public InviteController(ILogger<InviteController> logger, IInviteBusiness inviteBusiness)
    {
        _logger = logger;
        _inviteBusiness = inviteBusiness;
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
}