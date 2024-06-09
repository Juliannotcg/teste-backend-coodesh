using Coodesh.Challenge.Pokemon.WebApi.Shared.Data.Interfaces;
using Coodesh.Challenge.Pokemon.WebApi.Shared.Configuration.Interfaces;
using Coodesh.Challenge.Pokemon.WebApi.Shared.Data;

namespace Coodesh.Challenge.Pokemon.WebApi.Shared.DependencyInjection;

public static class DbContextBootStrapper
{
    public static IServiceCollection InstallSqLite(this IServiceCollection services, IAppSettings appSettings)
    {
        services.AddSingleton<ISqLiteDatabaseContext>(x => new SqLiteDatabaseContext());
        return services;
    }
}
