using Modules.Common.Domain.DomainEvent;

namespace Modules.Users.Domain.DomainEvents;

public class UnverifiedUserCreatedDomainEvent : DomainEvent
{
    public Guid UserId { get; private set; }
    public UnverifiedUserCreatedDomainEvent(Guid UserId)
    {
        this.UserId = UserId;
    }
}
