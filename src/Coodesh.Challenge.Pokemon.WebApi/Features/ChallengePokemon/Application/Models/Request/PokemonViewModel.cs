using Coodesh.Challenge.Pokemon.WebApi.Features.ChallengePokemon.Adapters.ExternalService.Models;

namespace Coodesh.Challenge.Pokemon.WebApi.Features.ChallengePokemon.Application.Models.Request;

public record PokemonViewModel(int Id, string Name, string ImageFront, string ImageBack, double Experience)
{
    public static explicit operator PokemonViewModel(ApiPokemonResponse apiPokemonResponse) => new(
            apiPokemonResponse.id,
            apiPokemonResponse.name,
            apiPokemonResponse.sprites.front_default,
            apiPokemonResponse.sprites.back_default,
            apiPokemonResponse.base_experience);
}