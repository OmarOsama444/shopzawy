using Modules.Users.Domain;
using Modules.Users.Domain.Entities;

namespace Modules.Users.Application.Abstractions;

public interface IJwtProvider
{
    Task<string> GenerateAccesss(User user);
    Task<string> GenerateGuestAccess(Guid guid);
    string GenerateReferesh();
}
