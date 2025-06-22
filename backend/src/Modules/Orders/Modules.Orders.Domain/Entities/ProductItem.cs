using Common.Domain.Entities;
using Modules.Orders.Domain.DomainEvents;

namespace Modules.Orders.Domain.Entities;

public class ProductItem : Entity
{
    public Guid Id { get; set; }
    public string StockKeepingUnit { get; set; } = string.Empty;
    public int QuantityInStock { get; set; }
    public List<string> ImageUrls { get; set; } = [];
    public float Weight { get; set; }
    public float Width { get; set; }
    public float Length { get; set; }
    public float Height { get; set; }
    public float Price { get; set; }
    public Guid ProductId { get; set; }
    public DateTime CreatedOn { get; set; }
    public virtual Product Product { get; set; } = default!;
    public virtual ICollection<ProductItemOptions> ProductItemOptions { get; set; } = [];
    public static ProductItem Create(
        string sku,
        int QuantityInStock,
        float price,
        float width,
        float length,
        float height,
        float weight,
        Guid productId,
        ICollection<string> urls
        )
    {
        ProductItem productItem = new()
        {
            Id = Guid.NewGuid(),
            StockKeepingUnit = sku,
            QuantityInStock = QuantityInStock,
            ImageUrls = urls.ToList(),
            Price = price,
            Width = width,
            Length = length,
            Height = height,
            Weight = weight,
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
        float? width,
        float? length,
        float? height,
        float? weight,
        ICollection<string>? AddUrls = null,
        ICollection<string>? RemoveUrls = null
    )
    {
        if (sku != null)
            this.StockKeepingUnit = sku;
        if (QuantityInStock.HasValue)
            this.QuantityInStock = QuantityInStock.Value;
        if (price.HasValue)
            this.Price = price.Value;
        if (width.HasValue)
            this.Width = width.Value;
        if (length.HasValue)
            this.Length = length.Value;
        if (height.HasValue)
            this.Height = height.Value;
        if (weight.HasValue)
            this.Weight = weight.Value;
        if (AddUrls != null && AddUrls.Count > 0)
            this.ImageUrls.AddRange(AddUrls);
        if (RemoveUrls != null && RemoveUrls.Count > 0)
            foreach (var url in RemoveUrls)
            {
                if (this.ImageUrls.Contains(url))
                {
                    this.ImageUrls.Remove(url);
                }
            }
    }
}
