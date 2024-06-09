namespace Coodesh.Challenge.Pokemon.WebApi.Shared.Caching.Interfaces;

public interface IMemoryCacheRepository<T> where T : class
{
    T? Get(string key);

    void Upsert(string key, T value);

    void DeleteByKeys(List<string> keys);
}