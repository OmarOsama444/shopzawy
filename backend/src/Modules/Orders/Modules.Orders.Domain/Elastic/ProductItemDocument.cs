namespace Modules.Orders.Domain.Elastic;

public class ProductItemDocument
{

    public string Id { get; set; } = string.Empty;
    public float Price { get; set; }
    public List<Variation<float>> NumericVariations { get; set; } = [];
    public List<Variation<string>> StringVariations { get; set; } = [];
    public static ProductItemDocument Create(Guid Id, float Price, List<Variation<float>> NumericVariations, List<Variation<string>> StringVariations)
    {
        return new ProductItemDocument()
        {
            Id = Id.ToString(),
            Price = Price,
            NumericVariations = NumericVariations,
            StringVariations = StringVariations,
        };
    }
}
