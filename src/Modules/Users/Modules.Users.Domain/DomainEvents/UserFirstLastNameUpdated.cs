using Modules.Common.Domain.DomainEvent;

namespace Modules.Users.Domain;

public class UserFirstLastNameUpdated : DomainEvent
{
    public Guid UserId { get; init; }
    public UserFirstLastNameUpdated(Guid UserId)
    {
        this.UserId = UserId;
    }
}
