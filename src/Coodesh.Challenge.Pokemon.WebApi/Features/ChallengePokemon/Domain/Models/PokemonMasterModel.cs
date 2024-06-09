using Coodesh.Challenge.Pokemon.WebApi.Features.ChallengePokemon.Domain.Models;

namespace Coodesh.Challenge.Pokemon.WebApi.Features.MetricsCollector.Domain.Models;

public class PokemonMasterModel
{
    public PokemonMasterModel(string name, int age, string cPF)
    {
        Name = name;
        Age = age;
        CPF = cPF;
    }

    public PokemonMasterModel(int id, string name, int age, string cPF)
    {
        Id = id;
        Name = name;
        Age = age;
        CPF = cPF;
    }

    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public int Age { get; private set; }
    public string CPF { get; private set; } = string.Empty;

    public List<PokemonCapturedModel>? PokemonCaptured { get; set; }
}