
namespace Coodesh.Challenge.Pokemon.WebApi.Features.ChallengePokemon.Adapters.ExternalService.Models;

public class ApiPokemonResponse
{
    public int id { get; set; }
    public int base_experience { get; set; }
    public string name { get; set; } = string.Empty;
    public required Sprites sprites { get; set; }
}


public class Sprites
{
    public string front_default { get; set; } = string.Empty;
    public string back_default { get; set; } = string.Empty;
}
