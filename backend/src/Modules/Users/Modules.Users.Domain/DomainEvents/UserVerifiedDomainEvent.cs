using Modules.Common.Domain.DomainEvent;

namespace Modules.Users.Domain;

public class UserVerifiedDomainEvent : DomainEvent
{
    public Guid UserId { get; init; }
    public UserVerifiedDomainEvent(Guid UserId)
    {
        this.UserId = UserId;
    }
}