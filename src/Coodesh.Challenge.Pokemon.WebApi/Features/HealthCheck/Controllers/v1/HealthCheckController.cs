using System.Net;
using Coodesh.Challenge.Pokemon.WebApi.Features.HealthCheck.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Swashbuckle.AspNetCore.Annotations;

namespace Coodesh.Challenge.Pokemon.WebApi.Features.HealthCheck.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[ControllerName("health-check")]
[Route("v{version:apiVersion}/health")]

public class HealthCheckController : ControllerBase
{
    private readonly HealthCheckService _healthCheck;

    public HealthCheckController(HealthCheckService healthCheck) => _healthCheck = healthCheck;

    [SwaggerOperation(Summary = "Health check of api")]
    [SwaggerResponse((int)HttpStatusCode.OK, Description = "Successful health check", Type = typeof(CustomHealthReport), ContentTypes = ["application/json"])]
    [SwaggerResponse((int)HttpStatusCode.ServiceUnavailable, Description = "Service unavailable for health check", Type = typeof(CustomHealthReport), ContentTypes = ["application/json"])]
    [HttpGet(Name = "get-health-check")]
    public async Task<IActionResult> GetHealthCheckAsync()
    {
        var healthReport = await _healthCheck.CheckHealthAsync(CancellationToken.None);

        var customHealthReport = new CustomHealthReport(healthReport.Entries, healthReport.Status, healthReport.TotalDuration);

        if (customHealthReport.Status == HealthStatus.Healthy)
        {
            return Ok(customHealthReport);
        }

        return StatusCode((int)HttpStatusCode.ServiceUnavailable, customHealthReport);
    }
}