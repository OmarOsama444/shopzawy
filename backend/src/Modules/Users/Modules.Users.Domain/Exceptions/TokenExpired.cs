using Modules.Common.Domain.Exceptions;

namespace Modules.Users.Domain.Exceptions;

public class TokenExpired : NotAuthorizedException
{
    public TokenExpired(string token) : base("Token.Invalid", $"Token with value {token} Expired")
    {
    }
}
