using Modules.Common.Domain.Entities;

namespace Modules.Orders.Domain.Entities;

public class SpecificationOption : Entity
{
    public Guid Id { get; private set; }
    public Guid SpecificationId { get; private set; }
    public string? StringValue { get; private set; }
    public double? NumberValue { get; private set; }
    public bool? BoolValue { get; private set; }
    public string DataType { get; private set; } = string.Empty;
    public string Value { get; private set; } = string.Empty;
    public virtual Specification Specification { get; set; } = default!;
    public virtual ICollection<ProductItemOptions> ProductItemOptions { get; set; } = [];
    public static SpecificationOption Create(string dataType, string value, Guid SpecId)
    {
        var option = new SpecificationOption
        {
            SpecificationId = SpecId,
            DataType = dataType.ToLower(),
            Value = value
        };

        switch (option.DataType)
        {
            case "string":
                option.StringValue = value.ToString();
                break;
            case "number":
                option.NumberValue = Convert.ToDouble(value);
                break;
            case "boolean":
                option.BoolValue = Convert.ToBoolean(value);
                break;
        }

        return option;
    }
}
