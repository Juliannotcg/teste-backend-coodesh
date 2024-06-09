using System.Diagnostics.CodeAnalysis;
using Coodesh.Challenge.Pokemon.WebApi.Shared.Configuration.Interfaces;
using Coodesh.Challenge.Pokemon.WebApi.Shared.Extensions;
using Serilog.Events;

namespace Coodesh.Challenge.Pokemon.WebApi.Shared.Configuration;


public class AppSettings : IAppSettings
{
    private readonly IConfiguration _configuration;

    public string ApiPokemonUrlBase { get; set; }
    public string ConnectionString { get; set; }
    public LogEventLevel DefaultLogLevel { get; set; }

    public AppSettings(IConfiguration configuration)
    {
        _configuration = configuration;

        ConnectionString = GetSettingsKey("ConnectionString");
        ApiPokemonUrlBase = GetSettingsKey("ApiPokemonUrlBase");
        DefaultLogLevel = Enum.Parse<LogEventLevel>(GetSettingsKey("Logging:LogLevel:Default"));
    }

    private string GetSettingsKey(string key)
    {
        var environment = Environment.GetEnvironmentVariable(key);
        return !string.IsNullOrEmpty(environment) ? environment : _configuration.GetAppKey(key);
    }
}