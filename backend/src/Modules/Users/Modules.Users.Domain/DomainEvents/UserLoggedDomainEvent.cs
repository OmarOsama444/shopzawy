using Common.Domain.DomainEvent;

namespace Modules.Users.Domain;

public class UserLoggedDomainEvent : DomainEvent
{
    public Guid UserId { get; init; }
    public Guid GuestId { get; init; }
    public UserLoggedDomainEvent(Guid UserId, Guid GuestId)
    {
        this.UserId = UserId;
        this.GuestId = GuestId;
    }
}