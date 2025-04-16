using Modules.Common.Domain.DomainEvent;

namespace Modules.Users.Domain.DomainEvents;

public class SmsTokenCreatedDomainEvent : DomainEvent
{
    public Guid TokenId { get; set; }
    public SmsTokenCreatedDomainEvent(Guid TokenId)
    {
        this.TokenId = TokenId;
    }
}
