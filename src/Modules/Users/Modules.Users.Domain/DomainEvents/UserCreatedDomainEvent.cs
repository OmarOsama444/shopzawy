using Modules.Common.Domain.DomainEvent;

namespace Modules.Users.Domain;

public class UserCreatedDomainEvent : DomainEvent
{
    public Guid UserId { get; init; }
    public UserCreatedDomainEvent(Guid UserId)
    {
        this.UserId = UserId;
    }

}
