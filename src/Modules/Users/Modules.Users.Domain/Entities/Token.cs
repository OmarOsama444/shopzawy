using Modules.Common.Domain.Entities;
using Modules.Users.Domain.DomainEvents;
using Modules.Users.Domain.ValueObjects;

namespace Modules.Users.Domain.Entities;

public class Token : Entity
{
    public Guid Id { get; set; }
    public string Value { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public TokenType TokenType { get; private set; }
    public DateTime ExpireDateUtc { get; private set; }
    public virtual User User { get; private set; } = default!;
    public static Token Create(
        TokenType tokenType,
        int lifeTimeInMinutes,
        Guid userId,
        string value)
    {
        var token = new Token()
        {
            TokenType = tokenType,
            ExpireDateUtc = DateTime.UtcNow.AddMinutes(lifeTimeInMinutes),
            UserId = userId,
            Value = value,
        };
        if (TokenType.Email == token.TokenType)
            token.RaiseDomainEvent(new EmailTokenCreatedDomainEvent(value));
        return token;
    }
}
