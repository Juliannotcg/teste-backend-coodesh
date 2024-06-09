using Coodesh.Challenge.Pokemon.WebApi.Shared.ResponseErrors;
using Coodesh.Challenge.Pokemon.WebApi.Shared.ResponseErrors.Interfaces;

namespace Coodesh.Challenge.Pokemon.WebApi.Shared.DependencyInjection;

public static class ApiErrorBuilderBootStrapper
{
    public static IServiceCollection InstallErrorBuilder(this IServiceCollection services)
    {
        services.AddScoped<IApiErrorBuilder, ApiErrorBuilder>();
        return services;
    }
}
