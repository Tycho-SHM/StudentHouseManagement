using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SHM.ProfileService.Abstractions.Repositories;
using SHM.ProfileService.Model;

namespace SHM.ProfileService.API.Controllers;

[ApiController]
[Route("[controller]")]
public class HouseProfileController : ControllerBase
{
    private readonly ILogger<HouseProfileController> _logger;
    private readonly IHouseProfileRepository _houseProfileRepository;

    public HouseProfileController(ILogger<HouseProfileController> logger, IHouseProfileRepository houseProfileRepository)
    {
        _logger = logger;
        _houseProfileRepository = houseProfileRepository;
    }

    [HttpGet]
    public async Task<List<HouseProfile>> GetAll()
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
    public async Task<HouseProfile> Post(HouseProfile houseProfile)
    {
        return await _houseProfileRepository.Add(houseProfile);
    }
}