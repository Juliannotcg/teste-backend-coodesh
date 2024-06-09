using System.Diagnostics.CodeAnalysis;

namespace Coodesh.Challenge.Pokemon.WebApi.Shared.Configuration.Models;


public class ServiceBusConfig
{
    public string ConnectionString { get; set; } = string.Empty;
    public string Queue { get; set; } = string.Empty;
}