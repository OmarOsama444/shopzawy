using Modules.Common.Domain.DomainEvent;

namespace Modules.Users.Domain.DomainEvents;

public class EmailTokenCreatedDomainEvent : DomainEvent
{
    public string Token { get; private init; }
    public EmailTokenCreatedDomainEvent(string Token)
    {
        this.Token = Token;
    }
}
