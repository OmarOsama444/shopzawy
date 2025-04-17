using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Application.UseCases.UpdateProduct;

public record UpdateProductCommand(string productName,
    string longDescription,
    string shortDescription,
    string imageUrl,
    WeightUnit weightUnit,
    float weight,
    float price,
    DimensionUnit dimensionUnit,
    float width,
    float length,
    float height,
    ICollection<string> tags);
