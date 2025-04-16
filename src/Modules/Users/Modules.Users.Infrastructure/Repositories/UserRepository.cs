using System.Data.Common;
using System.Net.Http.Headers;
using Dapper;
using Modules.Common.Infrastructure;
using Modules.Users.Application.Abstractions;
using Modules.Users.Domain;
using Modules.Users.Infrastructure.Repositories;

namespace Modules.Users.Infrastructure;

public class UserRepository(IDbConnectionFactory dbConnectionFactory, UserDbContext userDbContext) :
    Repository<User, UserDbContext>(userDbContext),
    IUserRepository
{
    public async Task<User?> GetUserByEmail(string Email)
    {
        await using DbConnection sqlConnection = await dbConnectionFactory.CreateSqlConnection();
        string sqlQuery =
        $"""
        SELECT
        *
        FROM 
        {UsersModule.SchemaName}.Users
        WHERE
        Email = @Email
        """;
        User? user = await sqlConnection.QueryFirstOrDefaultAsync<User>(sqlQuery, new { Email });
        return user;
    }

    public async Task<User?> GetUserById(Guid id)
    {
        await using DbConnection sqlConnection = await dbConnectionFactory.CreateSqlConnection();
        string sqlQuery =
        $"""
        SELECT
        * 
        FROM
        {UsersModule.SchemaName}.Users
        WHERE
        id = @id 
        """;
        User? user = await sqlConnection.QueryFirstOrDefaultAsync<User>(sqlQuery, new { id });
        return user;
    }

    public async Task<User?> GetUserByPhone(string PhoneNumber)
    {
        await using DbConnection sqlConnection = await dbConnectionFactory.CreateSqlConnection();
        string sqlQuery =
        $"""
        SELECT
        *
        FROM 
        {UsersModule.SchemaName}.Users
        WHERE
        PhoneNumber = @PhoneNumber
        """;
        User? user = await sqlConnection.QueryFirstOrDefaultAsync<User>(sqlQuery, new { PhoneNumber });
        return user;
    }
}
