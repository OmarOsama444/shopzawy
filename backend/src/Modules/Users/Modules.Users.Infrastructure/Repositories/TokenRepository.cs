using System.Text.Json;
using Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Modules.Users.Application.Repositories;
using Modules.Users.Domain.Entities;
using Modules.Users.Domain.ValueObjects;

namespace Modules.Users.Infrastructure.Repositories;

public class TokenRepository(UsersDbContext usersDbContext)
    : Repository<Token, UsersDbContext>(usersDbContext), ITokenRepository
{
    public async Task<Token?> GetByTokenTypeAndValue(string value, params TokenType[] types)
    {
        var token = await context
            .tokens
            .FirstOrDefaultAsync(
                x => types.Contains(x.TokenType) &&
                x.Value == value
            );
        return token;
    }
}
