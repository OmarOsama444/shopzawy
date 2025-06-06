using Modules.Users.Application.Abstractions;
using Modules.Users.Domain.Entities;
using Modules.Users.Domain.ValueObjects;

namespace Modules.Users.Domain.Repositories;

public interface ITokenRepository : IRepository<Token>
{
    public Task<Token?> GetByTokenTypeAndValue(TokenType tokenType, string value);
}
