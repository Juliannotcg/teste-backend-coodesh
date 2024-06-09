using Coodesh.Challenge.Pokemon.WebApi.Features.ChallengePokemon.Adapters.Repository.Interfaces;
using Coodesh.Challenge.Pokemon.WebApi.Features.ChallengePokemon.Domain.Models;
using Coodesh.Challenge.Pokemon.WebApi.Shared.Data.Interfaces;
using Coodesh.Challenge.Pokemon.WebApi.Shared.Data.Utils;
using Coodesh.Challenge.Pokemon.WebApi.Features.MetricsCollector.Domain.Models;

namespace Coodesh.Challenge.Pokemon.WebApi.Features.ChallengePokemon.Adapters.Repository;

public class PokemonMasterRepository : IPokemonMasterRepository
{
    private readonly ISqLiteDatabaseContext _sqLiteDatabaseContext;

    public PokemonMasterRepository(ISqLiteDatabaseContext sqLiteDatabaseContext) => _sqLiteDatabaseContext = sqLiteDatabaseContext;

    public async Task AddCapturePokemonAsync(int idPokemonMaster, int idPokemon, CancellationToken cancellationToken)
    {
        try
        {
            using var connection = _sqLiteDatabaseContext.SimpleDbConnection();
            await connection.OpenAsync(cancellationToken);
            var command = connection.CreateCommand();
            command.CommandText = Queries.InsertCapturePokemon;
            command.Parameters.AddWithValue("$idPokemonMaster", idPokemonMaster);
            command.Parameters.AddWithValue("$idPokemon", idPokemon);
            await command.ExecuteNonQueryAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    public async Task AddPokemonMasterAsync(PokemonMasterModel pokemonMasterModel, CancellationToken cancellationToken)
    {
        try
        {
            using var connection = _sqLiteDatabaseContext.SimpleDbConnection();
            await connection.OpenAsync(cancellationToken);
            var command = connection.CreateCommand();
            command.CommandText = Queries.InsertPokemonMaster;
            command.Parameters.AddWithValue("$name", pokemonMasterModel.Name);
            command.Parameters.AddWithValue("$age", pokemonMasterModel.Age);
            command.Parameters.AddWithValue("$cPF", pokemonMasterModel.CPF);
            await command.ExecuteNonQueryAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }


    public async Task<List<PokemonModel>> GetPokemonsByIdPokemonMasterAsync(int idPokemonMaster, CancellationToken cancellationToken)
    {
        using var connection = _sqLiteDatabaseContext.SimpleDbConnection();
        await connection.OpenAsync(cancellationToken);
        var command = connection.CreateCommand();
        command.CommandText = Queries.GetPokemonsCapturedByIdPokemonMaster;
        command.Parameters.AddWithValue("@idPokemonMaster", idPokemonMaster);

        var listPokemon = new List<PokemonModel>();

        using var reader = await command.ExecuteReaderAsync(cancellationToken);

        while (await reader.ReadAsync(cancellationToken))
        {
            var pokemonModel = new PokemonModel(
               reader.GetString(0),
               reader.GetString(1),
               reader.GetString(2),
               reader.GetDouble(3)
            );
            listPokemon.Add(pokemonModel);
        }

        return listPokemon;
    }

    public async Task<bool> PokemonMasterExistAsync(int id, CancellationToken cancellationToken)
    {
        using var connection = _sqLiteDatabaseContext.SimpleDbConnection();
        await connection.OpenAsync(cancellationToken);
        var command = connection.CreateCommand();
        command.CommandText = Queries.PokemonMasterExist;
        command.Parameters.AddWithValue("id", id);

        using var reader = await command.ExecuteReaderAsync();
        return reader.HasRows;
    }
}
