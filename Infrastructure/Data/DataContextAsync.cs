using System.Data;
using Npgsql;

namespace Infrastructure.Data;

public class DataContextAsync
{
    private const string ConnectionString = "Host=localhost; Username=postgres; Password=Fa1konm1; Database=Medium_db";

    public async Task<IDbConnection> GetConnectionAsync()
    {
        return new NpgsqlConnection(ConnectionString);
    }
}