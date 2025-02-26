using Microsoft.AspNetCore.Mvc;
using SHM.MessageQueues.Abstractions;

namespace SHM.TaskService.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TaskController : ControllerBase
{
    private readonly ILogger<TaskController> _logger;
    private readonly IMessageBrokerConnection _messageBrokerConnection;

    public TaskController(ILogger<TaskController> logger, IMessageBrokerConnection messageBrokerConnection)
    {
        _logger = logger;
        _messageBrokerConnection = messageBrokerConnection;
    }

    [HttpPost(Name = "SendMessageToNotificationService")]
    public async Task<IActionResult> PostMessage(string message)
    {
        await _messageBrokerConnection.PublishMessageToQueue("notifications", message);
        return Ok();
    }
}