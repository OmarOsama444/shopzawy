using Common.Domain.DomainEvent;

namespace Modules.Orders.Domain.DomainEvents;

public class SpecificationOptionCreatedDomainEvent : DomainEvent
{
    public Guid SpecId { get; private set; }
    public string Value { get; private set; } = string.Empty;
    public SpecificationOptionCreatedDomainEvent(Guid specId, string value)
    {
        this.SpecId = specId;
        this.Value = value;
    }
}
