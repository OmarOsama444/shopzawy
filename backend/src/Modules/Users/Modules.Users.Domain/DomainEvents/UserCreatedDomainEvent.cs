using Common.Domain.DomainEvent;

namespace Modules.Users.Domain;

public class UserCreatedDomainEvent : DomainEvent
{
    public Guid UserId { get; init; }
    public Guid GuestId { get; init; }
    public UserCreatedDomainEvent(Guid UserId, Guid GuestId)
    {
        this.UserId = UserId;
        this.GuestId = GuestId;
    }

}
