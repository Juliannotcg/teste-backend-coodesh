using Coodesh.Challenge.Pokemon.WebApi.Shared.Services;
using Coodesh.Challenge.Pokemon.WebApi.Shared.Services.Interfaces;

namespace Coodesh.Challenge.Pokemon.WebApi.Shared.DependencyInjection;

public static class RequestServiceBootStrapper
{
    public static IServiceCollection InstallRequestService(this IServiceCollection services)
    {
        services.AddTransient<RequestServiceLogger>();
        services.AddScoped(typeof(IRequestService<>), typeof(RequestService<>));

        return services;
    }
}
