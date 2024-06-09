using Coodesh.Challenge.Pokemon.WebApi.Shared.Caching;
using Coodesh.Challenge.Pokemon.WebApi.Shared.Caching.Interfaces;

namespace Coodesh.Challenge.Pokemon.WebApi.Shared.DependencyInjection;

public static class MemoryCacheRepositoryBootStrapper
{
    public static IServiceCollection InstallMemoryCache(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddSingleton(typeof(IMemoryCacheRepository<>), typeof(MemoryCacheRepository<>));

        return services;
    }
}