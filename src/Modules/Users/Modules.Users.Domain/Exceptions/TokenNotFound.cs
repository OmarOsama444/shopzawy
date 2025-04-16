using Modules.Common.Domain.Exceptions;

namespace Modules.Users.Domain.Exceptions;

public class TokenNotFound : NotFoundException
{
    public TokenNotFound(string token) : base("Token.NotFound", $"Token with value {token} not found")
    {
    }

}

