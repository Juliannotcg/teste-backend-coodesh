using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;
using Serilog.Exceptions.EntityFrameworkCore.Destructurers;

namespace Coodesh.Challenge.Pokemon.WebApi.Shared.DependencyInjection;


public static class LoggingBootStrapper
{
    public static void Register(this IHostBuilder hostBuilder)
        => hostBuilder.UseSerilog(
            (context, loggerConfig) =>
            {
                loggerConfig
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                    .MinimumLevel.Override("System", LogEventLevel.Warning)
                    .MinimumLevel.Override("Sqlite", LogEventLevel.Fatal)
                    .Enrich.WithExceptionDetails(new DestructuringOptionsBuilder()
                    .WithDefaultDestructurers()
                    .WithDestructurers(new[] { new DbUpdateExceptionDestructurer() }));
            }
        );
}