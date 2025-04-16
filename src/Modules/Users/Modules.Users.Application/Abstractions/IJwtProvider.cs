using Modules.Users.Domain;

namespace Modules.Users.Application.Abstractions;
public interface IJwtProvider
{
    string GenerateAccesss(User user);
    string GenerateReferesh();
    Task<(string accesstoken, string refreshtoken)> LoginUser(User user);
}
