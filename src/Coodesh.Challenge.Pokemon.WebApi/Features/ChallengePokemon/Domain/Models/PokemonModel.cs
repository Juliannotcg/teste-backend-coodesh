namespace Coodesh.Challenge.Pokemon.WebApi.Features.ChallengePokemon.Domain.Models;

public class PokemonModel
{
    public PokemonModel(int id, string name, string imageFront, string imageBack, double experience)
    {
        Id = id;
        Name = name;
        ImageFront = imageFront;
        ImageBack = imageBack;
        Experience = experience;
    }

    public PokemonModel(string name, string imageFront, string imageBack, double experience)
    {
        Name = name;
        ImageFront = imageFront;
        ImageBack = imageBack;
        Experience = experience;
    }

    public int Id { get; private set; }
    public string Name { get; private set; }
    public string ImageFront { get; private set; }
    public string ImageBack { get; private set; }
    public double Experience { get; private set; }

    public List<PokemonCapturedModel>? PokemonCaptured { get; set; }
}
