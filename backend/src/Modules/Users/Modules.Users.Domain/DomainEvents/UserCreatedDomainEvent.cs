using Common.Domain.DomainEvent;

namespace Modules.Users.Domain;

public class UserCreatedDomainEvent(Guid UserId) : DomainEvent
{
    public Guid UserId { get; init; } = UserId;
}
