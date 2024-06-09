using Microsoft.Data.Sqlite;

namespace Coodesh.Challenge.Pokemon.WebApi.Shared.Data.Interfaces;

public interface ISqLiteDatabaseContext
{
    public SqliteConnection SimpleDbConnection();
}
