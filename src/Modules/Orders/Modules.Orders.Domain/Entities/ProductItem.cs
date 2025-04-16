using Modules.Common.Domain.Entities;

namespace Modules.Orders.Domain.Entities;
public class ProductItem : Entity
{
    public Guid Id { get; private set; }
    public string StockKeepingUnit { get; private set; } = string.Empty;
    public int QuantityInStock { get; private set; }
    public string ImageUrl { get; private set; } = string.Empty;
    public float Price { get; private set; }
    public Guid ProductId { get; private set; }
    public virtual Product Product { get; set; } = default!;
    public virtual ICollection<ProductItemOptions> ProductItemOptions { get; set; } = [];
}
