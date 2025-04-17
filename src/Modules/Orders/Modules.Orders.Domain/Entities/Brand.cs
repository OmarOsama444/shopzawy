using Modules.Common.Domain.Entities;

namespace Modules.Orders.Domain.Entities;

public class Brand : Entity
{
    public string BrandName { get; set; } = string.Empty;
    public string LogoUrl { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool Featured { get; set; } = false;
    public bool Active { get; set; } = false;
    public DateTime CreatedOn { get; set; }
    public virtual ICollection<Product> Products { get; set; } = [];
    public static Brand Create(string brandName, string logoUrl, string description, bool? featured, bool? active)
    {
        return new Brand
        {
            BrandName = brandName,
            LogoUrl = logoUrl,
            Description = description,
            Featured = featured ?? false,
            Active = active ?? true,
            CreatedOn = DateTime.UtcNow
        };
    }

    public void Update(string? description, string? logoUrl, bool? featured, bool? active)
    {

        if (!string.IsNullOrWhiteSpace(description))
            Description = description;

        if (!string.IsNullOrWhiteSpace(logoUrl))
            LogoUrl = logoUrl;

        if (featured.HasValue)
            Featured = featured.Value;

        if (active.HasValue)
            Active = active.Value;
    }
}