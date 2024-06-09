using Coodesh.Challenge.Pokemon.WebApi.Shared.Configuration.Interfaces;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Coodesh.Challenge.Pokemon.WebApi.Shared.DependencyInjection;

public static class HealthCheckBootStrapper
{
    public static IServiceCollection InstallHealthChecks(this IServiceCollection services, IAppSettings appSettings)
    {
        services
            .AddHealthChecks()
            .AddCheck("SQLite", () =>
            {
                try
                {
                    using var connection = new SqliteConnection(appSettings.ConnectionString);
                    connection.Open();
                }
                catch (Exception)
                {
                    return HealthCheckResult.Unhealthy();
                }

                return HealthCheckResult.Healthy();
            });

        return services;
    }
}
