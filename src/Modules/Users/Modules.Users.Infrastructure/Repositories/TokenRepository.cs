using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Modules.Users.Domain.Entities;
using Modules.Users.Domain.Repositories;
using Modules.Users.Domain.ValueObjects;

namespace Modules.Users.Infrastructure.Repositories;

public class TokenRepository(UsersDbContext usersDbContext)
    : Repository<Token, UsersDbContext>(usersDbContext), ITokenRepository
{
    public async Task<Token?> GetByTokenTypeAndValue(TokenType tokenType, string value)
    {
        var token = await context
            .tokens
            .FirstOrDefaultAsync(
                x => x.TokenType == tokenType &&
                x.Value == value
            );
        return token;
    }
}
