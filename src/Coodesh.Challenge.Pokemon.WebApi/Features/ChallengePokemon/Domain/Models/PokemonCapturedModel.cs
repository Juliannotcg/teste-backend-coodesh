using Coodesh.Challenge.Pokemon.WebApi.Features.MetricsCollector.Domain.Models;

namespace Coodesh.Challenge.Pokemon.WebApi.Features.ChallengePokemon.Domain.Models;

public class PokemonCapturedModel
{
    public PokemonCapturedModel(int id, int idPokemon, int idPokemonMaster)
    {
        Id = id;
        IdPokemon = idPokemon;
        IdPokemonMaster = idPokemonMaster;
    }
    public int Id { get; set; }
    public int IdPokemon { get; private set; }
    public int IdPokemonMaster { get; private set; }


    public virtual PokemonModel? Pokemon { get; set; }
    public virtual PokemonMasterModel? PokemonMaster { get; set; }
}
