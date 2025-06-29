namespace Modules.Orders.Domain.Elastic;

public class Variation<T>
{
    public string SpecId { get; set; } = string.Empty;
    public T Value { get; set; } = default!;
    public static Variation<T> Create(Guid SpecId, T Value)
    {
        return new Variation<T>
        {
            SpecId = SpecId.ToString(),
            Value = Value
        };
    }
}
