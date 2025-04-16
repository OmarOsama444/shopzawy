using Modules.Common.Domain.DomainEvent;

namespace Modules.Orders.Domain.DomainEvents;

public class SpecCreatedDomainEvent : DomainEvent
{
    public Guid SpecId { get; private set; }
    public SpecCreatedDomainEvent(Guid specId)
    {
        this.SpecId = specId;
    }
}
