using Microsoft.AspNetCore.Identity;
using Modules.Common.Domain.DomainEvent;
using Modules.Common.Domain.Entities;
using Modules.Users.Domain.DomainEvents;
using Modules.Users.Domain.ValueObjects;

namespace Modules.Users.Domain.Entities;

public class UserToken : IdentityUserToken<Guid>, IEntity
{
    public TokenType tokenType { get; private set; }
    public DateTime ExpireDateUtc { get; private set; }
    public User User { get; private set; } = default!;
    public bool Used { get; private set; }
    public static UserToken Create(
        TokenType tokenType,
        int lifeTimeInMinutes,
        Guid userId,
        string value)
    {
        var token = new UserToken()
        {
            tokenType = tokenType,
            ExpireDateUtc = DateTime.UtcNow.AddMinutes(lifeTimeInMinutes),
            UserId = userId,
            Value = value,
            Used = false
        };
        if (TokenType.Email == token.tokenType)
            token.RaiseDomainEvent(new EmailTokenCreatedDomainEvent(value));
        return token;
    }

    public void Use()
    {
        this.Used = true;
    }

    private readonly List<IDomainEvent> domainEvents = [];
    public IReadOnlyCollection<IDomainEvent> DomainEvents => domainEvents.ToList();
    public void ClearDomainEvents()
    {
        domainEvents.Clear();
    }
    public void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        domainEvents.Add(domainEvent);
    }
}
