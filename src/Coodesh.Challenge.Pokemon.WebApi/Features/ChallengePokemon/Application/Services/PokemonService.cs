using Coodesh.Challenge.Pokemon.WebApi.Features.ChallengePokemon.Adapters.ExternalService.Interfaces;
using Coodesh.Challenge.Pokemon.WebApi.Features.ChallengePokemon.Adapters.ExternalService.Models;
using Coodesh.Challenge.Pokemon.WebApi.Features.ChallengePokemon.Application.Interfaces;
using Coodesh.Challenge.Pokemon.WebApi.Features.ChallengePokemon.Application.Models.Request;

namespace Coodesh.Challenge.Pokemon.WebApi.Features.ChallengePokemon.Application.Services;

public class PokemonService : IPokemonService
{
    public readonly IApiPokemonExternalService _apiPokemonExternalService;
    private readonly ILogger<PokemonService> _logger;

    public PokemonService(IApiPokemonExternalService apiPokemonExternalService, ILogger<PokemonService> logger)
    {
        _apiPokemonExternalService = apiPokemonExternalService;
        _logger = logger;
    }

    public async Task<IEnumerable<PokemonViewModel>> GetListRandomPokemonAsync(CancellationToken cancellationToken)
    {
        var resultPokemons = await _apiPokemonExternalService.GetApiListRandomPokemonsAsync(cancellationToken);

        return ApiPokemonResponseToPokemonViewModel(resultPokemons);
    }

    public async Task<PokemonViewModel> GetPokemonByIdAsync(int id, CancellationToken cancellationToken)
    {
        var resultPokemon = await _apiPokemonExternalService.GetApiPokemonsByIdAsync(id, cancellationToken);
        return (PokemonViewModel)resultPokemon!;
    }

    private static IEnumerable<PokemonViewModel> ApiPokemonResponseToPokemonViewModel(IEnumerable<ApiPokemonResponse> listApiPokemonResponse)
        => listApiPokemonResponse.Select(x => new PokemonViewModel(x.id, x.name, x.sprites.front_default, x.sprites.back_default, x.base_experience));
}
