using Coodesh.Challenge.Pokemon.WebApi.Shared.Configuration.Interfaces;
using Coodesh.Challenge.Pokemon.WebApi.Shared.Extensions;
using Coodesh.Challenge.Pokemon.WebApi.Shared.Middlewares;

namespace Coodesh.Challenge.Pokemon.WebApi.Shared.DependencyInjection;

public static class Configuration
{
    public static void RunServicesBootStrappers(WebApplicationBuilder builder, IAppSettings appSettings)
    {
        LoggingBootStrapper.Register(builder.Host);

        builder.Services
            .InstallHealthChecks(appSettings)
            .InstallRequestService()
            .InstallChallengePokemonService(appSettings)
            .InstallMemoryCache()
            .InstallSqLite(appSettings)
            .InstallErrorBuilder();
    }

    public static void UseConfiguredServices(WebApplication app)
    {
        app.UseExceptionHandler(builder => builder.Run(ProblemDetailsMiddleware.HandleUnhandledExceptions));
        app.UseSwaggerDocumentation();
        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseCors();
    }
}
