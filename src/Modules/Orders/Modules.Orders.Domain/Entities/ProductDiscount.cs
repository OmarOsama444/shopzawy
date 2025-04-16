using Modules.Common.Domain.Entities;

namespace Modules.Orders.Domain.Entities;

public class ProductDiscount : Entity
{
    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public virtual Product Product { get; private set; } = default!;
    public Guid DiscountId { get; private set; }
    public virtual Discount Discount { get; private set; } = default!;
    public float SalePrice { get; private set; }
    public static ProductDiscount Create(Guid productId, Guid discountId, float SalePrice)
    {
        return new ProductDiscount()
        {
            Id = Guid.NewGuid(),
            ProductId = productId,
            DiscountId = discountId,
            SalePrice = SalePrice
        };
    }
}