using Microsoft.AspNetCore.Mvc;
using SHM.ProfileService.Model;

namespace SHM.ProfileService.API.Controllers;

[ApiController]
[Route("[controller]")]
public class InviteController : ControllerBase
{
    private readonly ILogger<InviteController> _logger;
    
    public InviteController(ILogger<InviteController> logger)
    {
        _logger = logger;
    }

    [HttpGet("Received/{userProfileId:guid}")]
    public async Task<ActionResult<List<Invite>>> Get(Guid userProfileId)
    {
        return await Task.FromResult(new List<Invite>());
    }
}