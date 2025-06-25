using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Domain.Elastic;

public class ProductDocument
{
    public Guid Id { get; set; }
    public LocalizedField Name { get; set; } = default!;
    public LocalizedField Description { get; set; } = default!;
    public List<Guid> CategoryIds { get; set; } = [];
    public List<Variation<float>> NumericVariations { get; set; } = [];
    public List<Variation<string>> StringVariations { get; set; } = [];
}

public class LocalizedField
{
    public string En { get; set; } = string.Empty;
    public string Ar { get; set; } = string.Empty;
}

public class Variation<T>
{
    public Guid SpecId { get; set; }
    public T Value { get; set; } = default!;
}
