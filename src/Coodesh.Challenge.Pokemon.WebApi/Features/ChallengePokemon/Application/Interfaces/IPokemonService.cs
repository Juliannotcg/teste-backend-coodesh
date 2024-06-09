using Coodesh.Challenge.Pokemon.WebApi.Features.ChallengePokemon.Application.Models.Request;

namespace Coodesh.Challenge.Pokemon.WebApi.Features.ChallengePokemon.Application.Interfaces;

public interface IPokemonService
{
    Task<IEnumerable<PokemonViewModel>> GetListRandomPokemonAsync(CancellationToken cancellationToken);
    Task<PokemonViewModel> GetPokemonByIdAsync(int id, CancellationToken cancellationToken);
}
