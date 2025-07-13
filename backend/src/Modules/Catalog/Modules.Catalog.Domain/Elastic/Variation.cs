namespace Modules.Catalog.Domain.Elastic;

public class Variation<T>
{
    public string SpecId { get; set; } = string.Empty;
    public T Value { get; set; } = default!;

}

public class ColorVariation : Variation<string>
{
    string? En { get; set; }
    string? Ar { get; set; }
}

public class StringVariation : Variation<string>
{
    public static StringVariation Create(Guid SpecId, string Value)
    {
        return new StringVariation
        {
            SpecId = SpecId.ToString(),
            Value = Value
        };
    }
}

public class NumericVariation : Variation<float>
{
    public static NumericVariation Create(Guid SpecId, float Value)
    {
        return new NumericVariation
        {
            SpecId = SpecId.ToString(),
            Value = Value
        };
    }
}

