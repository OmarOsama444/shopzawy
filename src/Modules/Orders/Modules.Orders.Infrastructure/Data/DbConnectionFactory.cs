using System.Data.Common;
using Microsoft.Data.SqlClient;
using Modules.Orders.Application.Abstractions;
using Npgsql;

namespace Modules.Orders.Infrastructure.Data;

public class DbConnectionFactory(string ConnectionString) : IDbConnectionFactory
{
    public async Task<DbConnection> CreateSqlConnection()
    {
        var connection = new NpgsqlConnection(ConnectionString);
        await connection.OpenAsync();
        return connection;
    }
}
