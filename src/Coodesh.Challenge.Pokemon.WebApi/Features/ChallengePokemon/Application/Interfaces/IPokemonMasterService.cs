using Coodesh.Challenge.Pokemon.WebApi.Features.ChallengePokemon.Application.Models.Request;
using Coodesh.Challenge.Pokemon.WebApi.Features.ChallengePokemon.Application.Models.Response;

namespace Coodesh.Challenge.Pokemon.WebApi.Features.ChallengePokemon.Application.Interfaces;

public interface IPokemonMasterService
{
    Task AddPokemonMasterAsync(PokemonMasterViewModel pokemonMaster, CancellationToken cancellationToken);
    Task RegisterCapturePokemonAsync(RegisterCapturePokemonViewModel capturePokemon, CancellationToken cancellationToken);
    Task<IEnumerable<PokemonCapturedViewModel>> GetCapturePokemonAsync(int idPokemonMaster, CancellationToken cancellationToken);
}
