using Coodesh.Challenge.Pokemon.WebApi.Shared.Caching.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Coodesh.Challenge.Pokemon.WebApi.Shared.Caching;

public class MemoryCacheRepository<T>(IMemoryCache cache) :
    IMemoryCacheRepository<T> where T :
    class
{

    public T? Get(string key)
    {
        cache.TryGetValue(key, out T? value);

        return value;
    }

    public void Upsert(string key, T value) => cache.Set(key, value);

    public void DeleteByKeys(List<string> keys)
    {
        foreach (var key in keys)
        {
            cache.Remove(key);
        }
    }
}