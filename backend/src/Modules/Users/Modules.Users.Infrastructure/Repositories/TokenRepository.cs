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
    public async Task<Token?> GetByTokenTypeAndValue(string value, TokenType type)
    {
        var token = await context
            .tokens
            .FirstOrDefaultAsync(
                x => x.TokenType == type &&
                x.Value == value
            );
        return token;
    }
}
