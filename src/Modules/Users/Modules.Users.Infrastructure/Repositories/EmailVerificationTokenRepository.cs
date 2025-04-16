using System.Data;
using System.Data.Common;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Modules.Common.Infrastructure;
using Modules.Users.Application.Abstractions;
using Modules.Users.Domain;
using Modules.Users.Domain.Entities;
using Modules.Users.Domain.Repositories;

namespace Modules.Users.Infrastructure.Repositories;

public class EmailVerificationTokenRepository(
    UserDbContext userDbContext,
    IDbConnectionFactory dbConnectionFactory) :
        Repository<EmailVerificationToken, UserDbContext>(userDbContext),
        IEmailVerificationTokenRepository
{



    public override async Task<EmailVerificationToken?> GetByIdAsync(object Id)
    {
        await using DbConnection connection = await dbConnectionFactory.CreateSqlConnection();
        string query =
        $"""
        SELECT
        *
        FROM
        {UsersModule.SchemaName}.EmailVerificationTokens
        WHERE 
        {nameof(EmailVerificationToken.Id)} = @Id
        """;
        return await connection.QueryFirstOrDefaultAsync<EmailVerificationToken>(query, new { Id });

    }


    public async Task<EmailVerificationToken?> GetByUserId(Guid id)
    {
        await using DbConnection connection = await dbConnectionFactory.CreateSqlConnection();
        string query =
        $"""
        SELECT
        *
        FROM
        {UsersModule.SchemaName}.EmailVerificationTokens
        WHERE 
        {nameof(EmailVerificationToken.UserId)} = @UserId
        """;
        return await connection.QueryFirstOrDefaultAsync<EmailVerificationToken>(query, new { UserId = id });
    }
}
