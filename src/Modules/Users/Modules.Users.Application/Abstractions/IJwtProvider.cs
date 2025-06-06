using Modules.Users.Domain;
using Modules.Users.Domain.Entities;

namespace Modules.Users.Application.Abstractions;

public interface IJwtProvider
{
    string GenerateAccesss(User user);
    string GenerateReferesh();
}
