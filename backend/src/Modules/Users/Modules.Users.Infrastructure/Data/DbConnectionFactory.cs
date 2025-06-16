using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using Modules.Users.Application.Abstractions;

namespace Modules.Users.Infrastructure.Data;

public class DbConnectionFactory(string ConnectionString) : IDbConnectionFactory
{
    public async Task<DbConnection> CreateSqlConnection()
    {
        var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();
        return connection;
    }
}
