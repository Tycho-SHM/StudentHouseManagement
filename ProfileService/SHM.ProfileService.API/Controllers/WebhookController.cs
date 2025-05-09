using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SHM.ProfileService.Abstractions.Business;
using Svix;
using Svix.Exceptions;
using Environment = System.Environment;

namespace SHM.ProfileService.API.Controllers;

[ApiController]
[Route("[controller]")]
public class WebhookController : ControllerBase
{
    private readonly ILogger<WebhookController> _logger;
    private readonly IUserProfileBusiness _userProfileBusiness;
    private readonly IHostEnvironment _environment;

    public WebhookController(ILogger<WebhookController> logger, IUserProfileBusiness userProfileBusiness, IHostEnvironment environment)
    {
        _logger = logger;
        _userProfileBusiness = userProfileBusiness;
        _environment = environment;
    }
    
    [HttpPost("DeleteUser")]
    public async Task<ActionResult> DeleteUser()
    {
        using var reader = new StreamReader(Request.Body);
        var payload = await reader.ReadToEndAsync();

        if (!_environment.IsDevelopment())
        {
            var headers = new WebHeaderCollection();
            headers.Set("svix-id", Request.Headers["svix-id"]);
            headers.Set("svix-timestamp", Request.Headers["svix-timestamp"]);
            headers.Set("svix-signature", Request.Headers["svix-signature"]);
        
            var webhookChecker = new Webhook("whsec_lqN95He/Msc5IXauPCLTaKekMztgph/b");

            try
            {
                webhookChecker.Verify(payload, headers);
            }
            catch (Exception e)
            {
                if (e is WebhookVerificationException)
                {
                    _logger.LogWarning("Unauthorized webhook!");
                    return Unauthorized();
                }
                return BadRequest();
            }
        }
        
        var jsonDocument = JsonDocument.Parse(payload);
        var userId = jsonDocument.RootElement.GetProperty("data").GetProperty("id").GetString();

        if (userId == null)
        {
            return BadRequest();
        }

        if (await _userProfileBusiness.Delete(userId))
        {
            return Ok();
        }

        return Problem();
    }
}