using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SHM.ProfileService.Abstractions.Repositories;
using SHM.ProfileService.Model;
using SHM.ProfileService.Model.House;

namespace SHM.ProfileService.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class HouseProfileController : ControllerBase
{
    private readonly ILogger<HouseProfileController> _logger;
    private readonly IHouseProfileRepository _houseProfileRepository;
    private readonly IUserProfileRepository _userProfileRepository;

    public HouseProfileController(ILogger<HouseProfileController> logger, IHouseProfileRepository houseProfileRepository, IUserProfileRepository userProfileRepository)
    {
        _logger = logger;
        _houseProfileRepository = houseProfileRepository;
        _userProfileRepository = userProfileRepository;
    }

    [HttpGet("GetAllJoinedBy/{userProfileId:guid}")]
    public async Task<List<HouseProfile>> GetAllJoinedBy(Guid userProfileId)
    {
        _logger.LogInformation(HttpContext.User.ToString());
        return await _houseProfileRepository.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<HouseProfile> GetById(Guid id)
    {
        return await _houseProfileRepository.GetById(id);
    }
    
    [HttpPost]
    public async Task<HouseProfile> Add(HouseProfile houseProfile)
    {
        return await _houseProfileRepository.Add(houseProfile);
    }

    [HttpPost("CreateWithName")]
    public async Task<ActionResult<HouseProfile>> CreateWithName(string houseName)
    {
        if (await _houseProfileRepository.ExistsByName(houseName))
        {
            return BadRequest();
        }

        var userProfileId = HttpContext.User.FindFirst("sub")!.Value;
        var userProfile = await _userProfileRepository.GetByUserId(userProfileId);
        
        return await _houseProfileRepository.Add(new HouseProfile
        {
            Name = houseName,
            Members =
            [
                new HouseMembership
                {
                    UserProfile = userProfile,
                }
            ]
        });
    }
}