using Coodesh.Challenge.Pokemon.WebApi.Features.ChallengePokemon.Domain.Models;
using Coodesh.Challenge.Pokemon.WebApi.Shared.Data.Interfaces;
using Coodesh.Challenge.Pokemon.WebApi.Shared.Data.Utils;

namespace Coodesh.Challenge.Pokemon.WebApi.Features.ChallengePokemon.Adapters.Repository.Interfaces;

public class PokemonRepository : IPokemonRepository
{
    private readonly ISqLiteDatabaseContext _sqLiteDatabaseContext;

    public PokemonRepository(ISqLiteDatabaseContext sqLiteDatabaseContext) => _sqLiteDatabaseContext = sqLiteDatabaseContext;

    public async Task AddPokemonAsync(PokemonModel pokemonModel, CancellationToken cancellationToken)
    {
        try
        {
            using var connection = _sqLiteDatabaseContext.SimpleDbConnection();
            await connection.OpenAsync(cancellationToken);
            var command = connection.CreateCommand();
            command.CommandText = Queries.InsertPokemon;
            command.Parameters.AddWithValue("$id", pokemonModel.Id);
            command.Parameters.AddWithValue("$name", pokemonModel.Name);
            command.Parameters.AddWithValue("$imageFront", pokemonModel.ImageFront);
            command.Parameters.AddWithValue("$imageBack", pokemonModel.ImageBack);
            command.Parameters.AddWithValue("$experience", pokemonModel.Experience);
            await command.ExecuteNonQueryAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}
