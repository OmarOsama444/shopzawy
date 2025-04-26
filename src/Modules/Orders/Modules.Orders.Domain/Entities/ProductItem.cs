using Modules.Common.Domain.Entities;
using Modules.Orders.Domain.DomainEvents;

namespace Modules.Orders.Domain.Entities;

public class ProductItem : Entity
{
    public Guid Id { get; set; }
    public string StockKeepingUnit { get; set; } = string.Empty;
    public int QuantityInStock { get; set; }
    public ICollection<string> ImageUrls { get; set; } = [];
    public float Price { get; set; }
    public Guid ProductId { get; set; }
    public DateTime CreatedOn { get; set; }
    public virtual Product Product { get; set; } = default!;
    public virtual ICollection<ProductItemOptions> ProductItemOptions { get; set; } = [];
    public static ProductItem Create(
        string sku,
        int QuantityInStock,
        float price,
        Guid productId,
        ICollection<string> urls
        )
    {
        ProductItem productItem = new()
        {
            Id = Guid.NewGuid(),
            StockKeepingUnit = sku,
            QuantityInStock = QuantityInStock,
            ImageUrls = urls,
            Price = price,
            ProductId = productId,
            CreatedOn = DateTime.UtcNow
        };
        productItem.RaiseDomainEvent(new ProductItemCreatedDomainEvent(productItem.Id));
        return productItem;
    }
    public void Update(
        string? sku,
        int? QuantityInStock,
        float? price,
        ICollection<string>? urls
    )
    {
        if (sku != null)
            this.StockKeepingUnit = sku;
        if (QuantityInStock.HasValue)
            this.QuantityInStock = QuantityInStock.Value;
        if (price.HasValue)
            this.Price = price.Value;
        if (urls != null)
            this.ImageUrls = urls;
    }
}
