using Modules.Common.Domain.DomainEvent;

namespace Modules.Notifications.Domain.DomainEvents;

public class UserCreatedDomainEvent : DomainEvent
{
    public Guid UserId { get; private set; }
    public UserCreatedDomainEvent(Guid UserId)
    {
        this.UserId = UserId;
    }
}
