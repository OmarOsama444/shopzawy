using Common.Domain.Entities;
using Modules.Catalog.Domain.Entities.Translation;

namespace Modules.Catalog.Domain.Entities;

public class Brand : Entity
{
    public Guid Id { get; set; }
    public string LogoUrl { get; set; } = string.Empty;
    public bool Featured { get; set; } = false;
    public bool Active { get; set; } = false;
    public DateTime CreatedOn { get; set; }
    public virtual ICollection<Product> Products { get; set; } = [];
    public virtual ICollection<BrandTranslation> BrandTranslations { get; set; } = [];
    public static Brand Create(string logoUrl, bool featured, bool active)
    {
        return new Brand
        {
            LogoUrl = logoUrl,
            Featured = featured,
            Active = active,
            CreatedOn = DateTime.UtcNow
        };
    }

    public void Update(string? logoUrl, bool? featured, bool? active)
    {
        if (!string.IsNullOrWhiteSpace(logoUrl))
            LogoUrl = logoUrl;

        if (featured.HasValue)
            Featured = featured.Value;

        if (active.HasValue)
            Active = active.Value;
    }
}