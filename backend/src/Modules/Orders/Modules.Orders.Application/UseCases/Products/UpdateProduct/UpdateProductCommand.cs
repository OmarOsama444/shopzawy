using Common.Application.Messaging;
using Common.Domain.ValueObjects;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Application.UseCases.Products.UpdateProduct;

public record LocalizedText(IDictionary<Language, string> translations);
public record UpdateProductCommand(
    Guid product_id,
    LocalizedText product_names,
    LocalizedText long_descriptions,
    LocalizedText short_descriptions,
    WeightUnit? weight_unit,
    DimensionUnit? dimension_unit,
    ICollection<string>? tags) : ICommand<Guid>;
