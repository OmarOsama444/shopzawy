using Modules.Common.Domain.Entities;

namespace Modules.Orders.Domain.Entities;

public class ProductItemOptions : Entity
{
    public Guid ProductItemId { get; private set; }
    public virtual ProductItem ProductItem { get; set; } = default!;
    public Guid CategorySpecificationOptionId { get; private set; }
    public virtual SpecificationOption SpecificationOptions { get; set; } = default!;
    public static ProductItemOptions Create(Guid productItem, Guid categorySpecOptionId)
    {
        return new ProductItemOptions { ProductItemId = productItem, CategorySpecificationOptionId = categorySpecOptionId };
    }
}
