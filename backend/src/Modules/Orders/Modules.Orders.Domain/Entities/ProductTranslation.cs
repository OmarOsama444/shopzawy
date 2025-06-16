using Modules.Common.Domain.Entities;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Domain.Entities;

public class ProductTranslation : Entity
{
    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public Language LangCode { get; private set; }
    public string ProductName { get; private set; } = string.Empty;
    public string LongDescription { get; private set; } = string.Empty;
    public string ShortDescription { get; private set; } = string.Empty;
    public Product Product { get; private set; } = default!;
    public static ProductTranslation Create(
        Guid productId,
        Language langCode,
        string productName,
        string longDescription,
        string shortDescription)
    {
        var productTranslation = new ProductTranslation
        {
            Id = Guid.NewGuid(),
            ProductId = productId,
            LangCode = langCode,
            ProductName = productName,
            LongDescription = longDescription,
            ShortDescription = shortDescription
        };
        return productTranslation;
    }

    public void Update(
        string? productName,
        string? longDescription,
        string? shortDescription)
    {
        if (!string.IsNullOrEmpty(productName))
            this.ProductName = productName;
        if (!string.IsNullOrEmpty(longDescription))
            this.LongDescription = longDescription;
        if (!string.IsNullOrEmpty(shortDescription))
            this.ShortDescription = shortDescription;
    }
}
