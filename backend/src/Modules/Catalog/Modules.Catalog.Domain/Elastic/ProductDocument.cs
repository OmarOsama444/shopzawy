using Modules.Catalog.Domain.Entities;

namespace Modules.Catalog.Domain.Elastic;

public class ProductDocument
{
    public string Id { get; set; } = string.Empty;
    public string ProductId { get; set; } = string.Empty;
    public string VendorId { get; set; } = string.Empty;
    public string BrandId { get; set; } = string.Empty;
    public List<string> ImageUrls { get; set; } = [];
    public List<int> CategoryIds { get; set; } = [];
    public LocalizedField Name { get; set; } = default!;
    public LocalizedField LongDescription { get; set; } = default!;
    public LocalizedField ShortDescription { get; set; } = default!;
    public bool InStock { get; set; } = default!;
    public float Price { get; set; }
    public List<NumericVariation> NumericVariations { get; set; } = [];
    public List<StringVariation> StringVariations { get; set; } = [];
    public List<StringVariation> ColorVariations { get; set; } = [];
    public static ProductDocument Create(
        Guid Id,
        Guid ProductId,
        Guid VendorId,
        Guid BrandId,
        List<int> CategoryIds,
        LocalizedField Name,
        LocalizedField LongDescription,
        LocalizedField ShortDescription,
        float Price,
        List<NumericVariation> NumericVariations,
        List<StringVariation> StringVariations,
        List<StringVariation> ColorVariations)
    {
        return new ProductDocument()
        {
            Id = Id.ToString(),
            VendorId = VendorId.ToString(),
            BrandId = BrandId.ToString(),
            CategoryIds = CategoryIds,
            Name = Name,
            LongDescription = LongDescription,
            ShortDescription = ShortDescription,
            ProductId = ProductId.ToString(),
            Price = Price,
            NumericVariations = NumericVariations,
            StringVariations = StringVariations,
            ColorVariations = ColorVariations
        };
    }
}
