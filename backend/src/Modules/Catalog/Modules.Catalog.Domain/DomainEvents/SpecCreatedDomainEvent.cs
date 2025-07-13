using Common.Domain.DomainEvent;

namespace Modules.Catalog.Domain.DomainEvents;

public class SpecCreatedDomainEvent(Guid specId) : DomainEvent
{
    public Guid SpecId { get; private set; } = specId;
}
