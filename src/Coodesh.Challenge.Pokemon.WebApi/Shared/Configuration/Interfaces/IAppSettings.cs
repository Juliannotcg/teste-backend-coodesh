using Serilog.Events;

namespace Coodesh.Challenge.Pokemon.WebApi.Shared.Configuration.Interfaces;

public interface IAppSettings
{
    string ConnectionString { get; set; }
    public string ApiPokemonUrlBase { get; set; }

    LogEventLevel DefaultLogLevel { get; set; }
}