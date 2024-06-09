using Coodesh.Challenge.Pokemon.WebApi.Features.ChallengePokemon.Adapters.ExternalService.Interfaces;
using Coodesh.Challenge.Pokemon.WebApi.Features.ChallengePokemon.Adapters.ExternalService.Models;
using Coodesh.Challenge.Pokemon.WebApi.Shared.Caching.Interfaces;
using Coodesh.Challenge.Pokemon.WebApi.Shared.Services.Interfaces;

namespace Coodesh.Challenge.Pokemon.WebApi.Features.ChallengePokemon.Adapters.ExternalService;

public class ApiPokemonExternalService : IApiPokemonExternalService
{
    private readonly IRequestService<ApiPokemonExternalService> _requestService;
    private readonly ILogger<ApiPokemonExternalService> _logger;
    private readonly IMemoryCacheRepository<ApiPokemonResponse> _memoryCacheRepository;

    public ApiPokemonExternalService(
        IRequestService<ApiPokemonExternalService> requestService,
        ILogger<ApiPokemonExternalService> logger,
        IMemoryCacheRepository<ApiPokemonResponse> memoryCacheRepository)
    {
        _requestService = requestService;
        _logger = logger;
        _memoryCacheRepository = memoryCacheRepository;
    }

    public async Task<IEnumerable<ApiPokemonResponse>> GetApiListRandomPokemonsAsync(CancellationToken cancellationToken, int quantity = 10)
    {
        var random = new Random();
        var listPokemons = new List<ApiPokemonResponse>(10);
        for (int i = 0; i < 899; i++)
        {
            int pokemonId = random.Next(1, 899);
            var pokemon = await GetPokemonApiByIdAsync(pokemonId, cancellationToken);

            if (pokemon == null)
            {
                continue;
            }

            listPokemons.Add(pokemon);
            if (listPokemons.Count == 10)
            {
                break;
            }
        }

        return listPokemons;
    }


    public async Task<ApiPokemonResponse?> GetApiPokemonsByIdAsync(int id, CancellationToken cancellationToken)
    {
        var pokemon = GetPokemonMemoryCache(id);

        pokemon ??= await GetPokemonApiByIdAsync(id, cancellationToken);

        return pokemon;
    }


    private async Task<ApiPokemonResponse?> GetPokemonApiByIdAsync(int id, CancellationToken cancellationToken)
    {
        var response = await _requestService.GetAsync<ApiPokemonResponse>(
               id.ToString(),
               null,
               cancellationToken: cancellationToken);

        if (response is not null)
        {
            AddPokemonMemoryCache(response);
        }

        return response;
    }


    private void AddPokemonMemoryCache(ApiPokemonResponse pokemon)
    {
        string key = pokemon.id.ToString();
        var result = _memoryCacheRepository.Get(key);

        if (result is null)
        {
            _memoryCacheRepository.Upsert(key, pokemon);
        }
    }

    private ApiPokemonResponse? GetPokemonMemoryCache(int id)
    {
        string key = id.ToString();
        return _memoryCacheRepository.Get(key);
    }
}
