using System.Data.Common;
using Microsoft.Data.SqlClient;
using Modules.Catalog;
using Modules.Catalog.Application.Abstractions;
using Npgsql;

namespace Modules.Catalog.Infrastructure.Data;

public class DbConnectionFactory(string ConnectionString) : IDbConnectionFactory
{
    public async Task<DbConnection> CreateSqlConnection()
    {
        var connection = new NpgsqlConnection(ConnectionString);
        await connection.OpenAsync();
        return connection;
    }
}
