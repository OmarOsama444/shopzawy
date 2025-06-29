namespace Modules.Orders.Domain.Elastic;

public class ProductDocument
{
    public string Id { get; set; } = string.Empty;
    public string VendorId { get; set; } = string.Empty;
    public string BrandId { get; set; } = string.Empty;
    public List<string> CategoryIds { get; set; } = [];
    public LocalizedField Name { get; set; } = default!;
    public LocalizedField LongDescription { get; set; } = default!;
    public LocalizedField ShortDescription { get; set; } = default!;
    public List<ProductItemDocument> ProductItemDocuments { get; set; } = [];
    public static ProductDocument Create(Guid Id, Guid VendorId, Guid BrandId, List<Guid> CategoryIds, LocalizedField Name, LocalizedField LongDescription, LocalizedField ShortDescription, List<ProductItemDocument> ProductItemDocuments)
    {
        return new ProductDocument()
        {
            Id = Id.ToString(),
            VendorId = VendorId.ToString(),
            BrandId = BrandId.ToString(),
            CategoryIds = [.. CategoryIds.Select(x => x.ToString())],
            ProductItemDocuments = ProductItemDocuments,
            Name = Name,
            LongDescription = LongDescription,
            ShortDescription = ShortDescription
        };
    }
}
