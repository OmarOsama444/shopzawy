using Common.Domain.DomainEvent;

namespace Modules.Orders.Domain.DomainEvents;

public class ProductItemOptionColorCreatedDomainEvent(Guid productItemId, Guid SpecificationID, string ColorCode) : DomainEvent
{
    public Guid ProductItemId { get; set; } = productItemId;
    public Guid SpecificationId { get; set; } = SpecificationID;
    public string ColorCode { get; set; } = ColorCode;
}
