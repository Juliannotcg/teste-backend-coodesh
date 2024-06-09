using Coodesh.Challenge.Pokemon.WebApi.Features.ChallengePokemon.Domain.Models;
using Coodesh.Challenge.Pokemon.WebApi.Features.MetricsCollector.Domain.Models;

namespace Coodesh.Challenge.Pokemon.WebApi.Features.ChallengePokemon.Adapters.Repository.Interfaces;

public interface IPokemonMasterRepository
{
    Task AddPokemonMasterAsync(PokemonMasterModel pokemonMasterModel, CancellationToken cancellationToken);
    Task<List<PokemonModel>> GetPokemonsByIdPokemonMasterAsync(int idPokemonMaster, CancellationToken cancellationToken);
    Task<bool> PokemonMasterExistAsync(int id, CancellationToken cancellationToken);
    Task AddCapturePokemonAsync(int idPokemonMaster, int idPokemon, CancellationToken cancellationToken);
}
