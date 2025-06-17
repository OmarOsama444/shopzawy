using Common.Domain.Entities;

namespace Modules.Orders.Domain.Entities;

public class ProductItemOptions : Entity
{
    public Guid ProductItemId { get; private set; }
    public virtual ProductItem ProductItem { get; set; } = default!;
    public Guid SpecificationId { get; private set; }
    public string Value { get; private set; } = string.Empty;
    public virtual SpecificationOption SpecificationOptions { get; set; } = default!;
    public static ProductItemOptions Create(Guid productItem, Guid specificationId, string value)
    {
        return new ProductItemOptions
        {
            ProductItemId = productItem,
            SpecificationId = specificationId,
            Value = value
        };
    }
}
