namespace Modules.Orders.Domain.Elastic;

public class ProductDocument
{
    public string Id { get; set; } = string.Empty;
    public string ProductId { get; set; } = string.Empty;
    public string VendorId { get; set; } = string.Empty;
    public string BrandId { get; set; } = string.Empty;
    public List<string> ImageUrls { get; set; } = [];
    public List<string> CategoryIds { get; set; } = [];
    public LocalizedField Name { get; set; } = default!;
    public LocalizedField LongDescription { get; set; } = default!;
    public LocalizedField ShortDescription { get; set; } = default!;
    public float Price { get; set; }
    public List<Variation<float>> NumericVariations { get; set; } = [];
    public List<Variation<string>> StringVariations { get; set; } = [];
    public static ProductDocument Create(
        Guid Id,
        Guid ProductId,
        Guid VendorId,
        Guid BrandId,
        List<Guid> CategoryIds,
        LocalizedField Name,
        LocalizedField LongDescription,
        LocalizedField ShortDescription,
        float Price,
        List<Variation<float>> NumericVariations,
        List<Variation<string>> StringVariations)
    {
        return new ProductDocument()
        {
            Id = Id.ToString(),
            VendorId = VendorId.ToString(),
            BrandId = BrandId.ToString(),
            CategoryIds = [.. CategoryIds.Select(x => x.ToString())],
            Name = Name,
            LongDescription = LongDescription,
            ShortDescription = ShortDescription,
            ProductId = ProductId.ToString(),
            Price = Price,
            NumericVariations = NumericVariations,
            StringVariations = StringVariations
        };
    }
}
