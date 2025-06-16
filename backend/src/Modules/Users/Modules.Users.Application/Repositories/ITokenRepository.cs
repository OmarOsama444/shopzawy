using Modules.Users.Application.Abstractions;
using Modules.Users.Domain.Entities;
using Modules.Users.Domain.ValueObjects;

namespace Modules.Users.Application.Repositories;

public interface ITokenRepository : IRepository<Token>
{
    public Task<Token?> GetByTokenTypeAndValue(string value, params TokenType[] types);
}
