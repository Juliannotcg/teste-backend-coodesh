using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Coodesh.Challenge.Pokemon.WebApi.Features.HealthCheck.Application.Models;


public class CustomHealthReport(
    IReadOnlyDictionary<string, HealthReportEntry> entries,
    HealthStatus status,
    TimeSpan totalDuration
)
{
    public IReadOnlyDictionary<string, HealthReportEntry> Entries { get; init; } = entries;
    public HealthStatus Status { get; init; } = status;
    public TimeSpan TotalDuration { get; init; } = totalDuration;
}