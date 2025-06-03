using Modules.Users.Domain;

namespace Modules.Users.Application.Abstractions;

public interface IJwtProvider
{
    string GenerateAccesss(User user);
    string GenerateReferesh();
}
