using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SHM.ProfileService.Abstractions.Business;
using SHM.ProfileService.Model;

namespace SHM.ProfileService.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserProfileController : ControllerBase
{
    private readonly ILogger<UserProfileController> _logger;
    private readonly IUserProfileBusiness _userProfileBusiness;

    public UserProfileController(ILogger<UserProfileController> logger, IUserProfileBusiness userProfileBusiness)
    {
        _logger = logger;
        _userProfileBusiness = userProfileBusiness;
    }

    [HttpGet("GetOwnProfile")]
    [Authorize]
    public async Task<ActionResult<UserProfile>> GetOwnProfile()
    {
        var user = HttpContext.User;
        if (!user.HasClaim(claim => claim.Type.Equals("sub")))
        {
            return BadRequest();
        }

        return await _userProfileBusiness.GetOrCreate(user.FindFirst("sub")!.Value);
    }

    [HttpPut]
    [Authorize]
    public async Task<ActionResult<UserProfile?>> UpdateProfile(UserProfile userProfile)
    {
        var user = HttpContext.User;
        if (!user.HasClaim(claim => claim.Type.Equals("sub")))
        {
            return BadRequest();
        }
        var userId = user.FindFirst("sub")!.Value;

        var existingProfile = await _userProfileBusiness.GetById(userProfile.Id);
        if (existingProfile == null)
        {
            return NoContent();
        }

        if (existingProfile.UserId != userId)
        {
            return Unauthorized();
        }
        
        return await _userProfileBusiness.Update(userProfile);
    }
}