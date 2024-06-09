using Coodesh.Challenge.Pokemon.WebApi.Features.ChallengePokemon.Domain.Models;

namespace Coodesh.Challenge.Pokemon.WebApi.Features.ChallengePokemon.Adapters.Repository.Interfaces;

public interface IPokemonRepository
{
    Task AddPokemonAsync(PokemonModel pokemonModel, CancellationToken cancellationToken);
}
