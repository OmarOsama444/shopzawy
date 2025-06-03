using Modules.Users.Application.Abstractions;
using Modules.Users.Domain.Entities;
using Modules.Users.Domain.ValueObjects;

namespace Modules.Users.Domain.Repositories;

public interface IUserTokenRepository
{
    public void Add(UserToken userToken);
    public Task<UserToken?> GetToken(string value, TokenType tokenType);
}
