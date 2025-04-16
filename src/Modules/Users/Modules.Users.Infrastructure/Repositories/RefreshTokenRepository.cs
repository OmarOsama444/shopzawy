using System.Data.Common;
using Dapper;
using Modules.Common.Infrastructure;
using Modules.Users.Application.Abstractions;
using Modules.Users.Domain;
using Modules.Users.Domain.Entities;

namespace Modules.Users.Infrastructure.Repositories;

public class RefreshTokenRepository(
        UserDbContext userDbContext,
        IDbConnectionFactory dbConnectionFactory) :
            Repository<RefreshToken, UserDbContext>(userDbContext),
            IRefreshRepository
{

    public async Task<RefreshToken?> GetByToken(string Token)
    {
        await using DbConnection sqlConnection = await dbConnectionFactory.CreateSqlConnection();
        string sqlQuery =
        $"""
        SELECT 
        *
        FROM 
        {UsersModule.SchemaName}.EmailVerificationTokens
        WHERE
        Token = @Token
        """;
        RefreshToken? refreshToken = await sqlConnection.QueryFirstOrDefaultAsync<RefreshToken>(sqlQuery, new { Token });
        return refreshToken;
    }
}
