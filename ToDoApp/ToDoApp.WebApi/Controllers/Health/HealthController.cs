using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.BLL.MediatR.Health.GetHealthStatus;
using ToDoApp.WebApi.Controllers.Base;

namespace ToDoApp.WebApi.Controllers.Health;

[Route("api/health")]
public class HealthController : BaseController
{
    private readonly ILogger<HealthController> _logger;

    public HealthController(IMediator mediator, ILogger<HealthController> logger) : base(mediator)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetHealthStatusQuery(), cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(new
            {
                timestamp = DateTime.UtcNow,
                level = "INFO",
                message = "Application and database are healthy",
                status = "healthy"
            });
        }

        var errorMessage = result.Reasons.Count > 0
            ? string.Join("; ", result.Reasons.Select(reason => reason.Message))
            : "Database connection failed";

        _logger.LogWarning("Health check failed: {ErrorMessage}", errorMessage);

        return StatusCode(StatusCodes.Status503ServiceUnavailable, new
        {
            timestamp = DateTime.UtcNow,
            level = "ERROR",
            message = errorMessage,
            status = "unhealthy"
        });
    }
}
