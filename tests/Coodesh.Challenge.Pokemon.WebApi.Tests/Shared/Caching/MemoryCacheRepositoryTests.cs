using Coodesh.Challenge.Pokemon.WebApi.Features.ChallengePokemon.Domain.Models;
using Coodesh.Challenge.Pokemon.WebApi.Shared.Caching;
using Microsoft.Extensions.Caching.Memory;

namespace Coodesh.Challenge.Pokemon.Shared.Caching;

public class MemoryCacheRepositoryTests
{
    public PokemonModel _pokemonModel;
    public readonly MemoryCache _memoryCache;

    public MemoryCacheRepositoryTests()
    {
        _pokemonModel = new PokemonModel("asd", "asd", "asd", 2);
        _memoryCache = new MemoryCache(new MemoryCacheOptions());
    }

    [Fact]
    public void Get_ReturnsCachedValue_WhenItemExists()
    {
        string _key = "teste-key";

        var cacheRepository = new MemoryCacheRepository<PokemonModel>(_memoryCache);

        _memoryCache.Set(_key, _pokemonModel, TimeSpan.FromSeconds(10));

        var actualValue = cacheRepository.Get(_key);

        actualValue.Should().NotBeNull();
        actualValue.Should().BeSameAs(_pokemonModel);
    }

    [Fact]
    public void Upsert_MustReturnObjectThatSavedInCache_WhenCacheExist()
    {
        string key = "key_upsert";
        var memoryCacheRepository = new MemoryCacheRepository<PokemonModel>(_memoryCache);

        memoryCacheRepository.Upsert(key, _pokemonModel);

        var actualValue = memoryCacheRepository.Get(key);

        actualValue.Should().NotBeNull();
    }

    [Fact]
    public void Remove_MustRemoveAllItemsInCache_WhenCacheExist()
    {
        string key1 = "key_upsert_1";
        string key2 = "key_upsert_2";
        var listKeys = new List<string> { key1, key2 };

        var metricValid1 = new PokemonModel("asd", "asd", "asd", 2);
        var metricValid2 = new PokemonModel("assd", "assd", "assd", 5);

        var memoryCacheRepository = new MemoryCacheRepository<PokemonModel>(_memoryCache);

        _pokemonModel = new PokemonModel("assd", "assd", "assd", 5);

        memoryCacheRepository.Upsert(key1, metricValid1);
        memoryCacheRepository.Upsert(key2, metricValid2);

        memoryCacheRepository.DeleteByKeys(listKeys);

        foreach (var key in listKeys)
        {
            var cachedValue = memoryCacheRepository.Get(key);
            cachedValue.Should().BeNull();
        }
    }
}