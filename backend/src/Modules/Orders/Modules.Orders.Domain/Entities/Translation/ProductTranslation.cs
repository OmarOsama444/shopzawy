using Common.Domain.Entities;
using Common.Domain.ValueObjects;
using Modules.Orders.Domain.DomainEvents;
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
        bool updated = false;
        if (!string.IsNullOrEmpty(productName) && this.ProductName != productName)
        {
            this.ProductName = productName;
            updated = true;
        }
        if (!string.IsNullOrEmpty(longDescription) && this.LongDescription != longDescription)
        {
            this.LongDescription = longDescription;
            updated = true;
        }
        if (!string.IsNullOrEmpty(shortDescription) && this.ShortDescription != shortDescription)
        {
            this.ShortDescription = shortDescription;
            updated = true;
        }
        if (updated)
        {
            this.RaiseDomainEvent(new ProductTranslationUpdatedDomainEvent(ProductId));
        }
    }
}
