using Modules.Users.Application.Abstractions;
using Modules.Users.Domain.Entities;

namespace Modules.Users.Domain;

public interface IRefreshRepository : IRepository<RefreshToken>
{
    Task<RefreshToken?> GetByToken(string Token);
}
