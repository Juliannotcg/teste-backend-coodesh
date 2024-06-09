using System.Text;
using Coodesh.Challenge.Pokemon.WebApi.Shared.Data.Interfaces;
using Coodesh.Challenge.Pokemon.WebApi.Shared.Data.Utils;
using Microsoft.Data.Sqlite;

namespace Coodesh.Challenge.Pokemon.WebApi.Shared.Data;


public class SqLiteDatabaseContext : ISqLiteDatabaseContext
{
    public SqLiteDatabaseContext()
    {
        CreateFileDb();
        CreateTable();
    }
    public string DbFile { get; set; } = "Pokedex.db";

    public SqliteConnection SimpleDbConnection()
    => new("Data Source=" + DbFile);

    internal bool CreateFileDb()
    {
        try
        {
            if (!File.Exists(DbFile))
            {
                StreamWriter file = new StreamWriter(DbFile, true, Encoding.Default);
                file.Dispose();
                return true;
            }

            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }

    internal void CreateTable()
    {
        try
        {
            if (!ExistTables())
            {
                using var connection = SimpleDbConnection();
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = Queries.CreateTables;
                command.ExecuteNonQuery();
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    public bool ExistTables()
    {
        using var connection = SimpleDbConnection();
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = Queries.ExistTables;
        command.Parameters.AddWithValue("$nomeTabela", "Pokemons");

        using var reader = command.ExecuteReader();
        return reader.Read();
    }
}
