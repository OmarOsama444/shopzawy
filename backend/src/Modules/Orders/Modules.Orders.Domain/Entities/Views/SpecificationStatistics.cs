using Common.Domain.Entities;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Domain.Entities.Views;

public class SpecificationStatistics : Entity
{
    public Guid Id { get; set; }
    public SpecDataType DataType { get; set; }
    public string Value { get; set; } = string.Empty;
    public int TotalProducts { get; set; }
    public DateTime CreatedOnUtc { get; set; } = DateTime.UtcNow;
    public static SpecificationStatistics Create(Guid id, SpecDataType dataType, string value, int totalProducts)
    {
        return new SpecificationStatistics
        {
            Id = id,
            DataType = dataType,
            Value = value,
            TotalProducts = totalProducts,
            CreatedOnUtc = DateTime.UtcNow
        };
    }
}
