using Coodesh.Challenge.Pokemon.WebApi.Features.ChallengePokemon.Adapters.ExternalService.Models;

namespace Coodesh.Challenge.Pokemon.WebApi.Features.ChallengePokemon.Adapters.ExternalService.Interfaces;

public interface IApiPokemonExternalService
{
    Task<IEnumerable<ApiPokemonResponse>> GetApiListRandomPokemonsAsync(CancellationToken cancellationToken, int quantity = 10);

    Task<ApiPokemonResponse?> GetApiPokemonsByIdAsync(int id, CancellationToken cancellationToken);
}
