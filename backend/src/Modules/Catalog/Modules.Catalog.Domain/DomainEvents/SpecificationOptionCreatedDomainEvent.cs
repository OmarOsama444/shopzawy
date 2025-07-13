using Common.Domain.DomainEvent;

namespace Modules.Catalog.Domain.DomainEvents;

public class SpecificationOptionCreatedDomainEvent(Guid specId, string value) : DomainEvent
{
    public Guid SpecId { get; private set; } = specId;
    public string Value { get; private set; } = value;
}
