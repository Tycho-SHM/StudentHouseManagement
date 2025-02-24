using Microsoft.AspNetCore.Mvc;

namespace NotificationService.API.Controllers;

[ApiController]
[Route("[controller]")]
public class SettingsController : ControllerBase
{
    private readonly ILogger<SettingsController> _logger;

    public SettingsController(ILogger<SettingsController> logger)
    {
        _logger = logger;
    }
    
    [HttpGet(Name = "GetSettings")]
    public IEnumerable<string> Get()
    {
        _logger.LogTrace("SettingsController::GetSettings called");
        return ["value1", "value2"];
    }
}