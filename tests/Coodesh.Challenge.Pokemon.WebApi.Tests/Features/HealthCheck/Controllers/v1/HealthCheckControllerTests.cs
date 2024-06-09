using System.Net;
using Coodesh.Challenge.Pokemon.WebApi.Features.HealthCheck.Application.Models;
using Coodesh.Challenge.Pokemon.WebApi.Features.HealthCheck.Controllers.v1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Moq;

namespace Coodesh.Challenge.Pokemon.WebApi.Tests.Features.HealthCheck.Controllers.v1;

public class HealthCheckControllerTests
{
    private readonly HealthCheckController _controller;
    private readonly Mock<HealthCheckService> _serviceMock;

    public HealthCheckControllerTests()
    {
        _serviceMock = new Mock<HealthCheckService>();
        _controller = new HealthCheckController(_serviceMock.Object);
    }

    [Fact]
    public async Task GetHealthCheckAsync_ShouldReturnServiceUnavailable_WhenOneOrMoreServicesAreUnhealthy()
    {
        var entries = new Dictionary<string, HealthReportEntry>()
        {
            {
                "TestService",
                new HealthReportEntry(
                    HealthStatus.Unhealthy,
                    "Unhealthy",
                    TimeSpan.FromSeconds(1),
                    null,
                    null
                )
            }
        };

        _serviceMock
            .Setup(_ => _.CheckHealthAsync(
                    It.IsAny<Func<HealthCheckRegistration, bool>>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(new HealthReport(entries, HealthStatus.Unhealthy, TimeSpan.FromSeconds(10)));

        var result = (await _controller.GetHealthCheckAsync()).As<ObjectResult>();

        result.Should().NotBeNull();
        result.StatusCode.Should().Be((int)HttpStatusCode.ServiceUnavailable);

        var resultBody = result.Value.As<CustomHealthReport>();

        resultBody.Should().NotBeNull();
        resultBody.Entries.Should().BeEquivalentTo(entries);
        resultBody.Status.Should().Be(HealthStatus.Unhealthy);
        resultBody.TotalDuration.Should().Be(TimeSpan.FromSeconds(10));
    }

    [Fact]
    public async Task GetHealthCheckAsync_ShouldOk_WhenAllServicesAreHealthy()
    {
        var entries = new Dictionary<string, HealthReportEntry>()
        {
            {
                "TestService",
                new HealthReportEntry(
                    HealthStatus.Healthy,
                    "Healthy",
                    TimeSpan.FromSeconds(1),
                    null,
                    null
                )
            }
        };

        _serviceMock
            .Setup(_ => _.CheckHealthAsync(
                    It.IsAny<Func<HealthCheckRegistration, bool>>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(new HealthReport(entries, HealthStatus.Healthy, TimeSpan.FromSeconds(20)));

        var result = (await _controller.GetHealthCheckAsync()).As<ObjectResult>();

        result.Should().NotBeNull();
        result.StatusCode.Should().Be((int)HttpStatusCode.OK);

        var resultBody = result.Value.As<CustomHealthReport>();

        resultBody.Should().NotBeNull();
        resultBody.Entries.Should().BeEquivalentTo(entries);
        resultBody.Status.Should().Be(HealthStatus.Healthy);
        resultBody.TotalDuration.Should().Be(TimeSpan.FromSeconds(20));
    }
}
