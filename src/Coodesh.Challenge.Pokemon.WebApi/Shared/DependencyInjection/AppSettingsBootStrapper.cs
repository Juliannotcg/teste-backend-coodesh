using Coodesh.Challenge.Pokemon.WebApi.Shared.Configuration;
using Coodesh.Challenge.Pokemon.WebApi.Shared.Configuration.Interfaces;

namespace Coodesh.Challenge.Pokemon.WebApi.Shared.DependencyInjection;

public static class AppSettingsBootStrapper
{
    internal static IAppSettings Register(WebApplicationBuilder builder)
        => new AppSettings(builder.Configuration);
}