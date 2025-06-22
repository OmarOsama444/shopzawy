using Common.Domain.Entities;
using Modules.Orders.Domain.DomainEvents;

namespace Modules.Orders.Domain.Entities;

public class ProductItemOptions : Entity
{
    public Guid Id { get; private set; }
    public virtual ProductItem ProductItem { get; set; } = default!;
    public Guid SpecificationId { get; private set; }
    public string Value { get; private set; } = string.Empty;
    public virtual SpecificationOption SpecificationOptions { get; set; } = default!;
    public static ProductItemOptions Create(Guid productItem, Guid specificationId, string value)
    {
        var x = new ProductItemOptions
        {
            Id = productItem,
            SpecificationId = specificationId,
            Value = value
        };
        x.RaiseDomainEvent(new ProductItemOptionCreatedDomainEvent(x.Id));
        return x;
    }
}
