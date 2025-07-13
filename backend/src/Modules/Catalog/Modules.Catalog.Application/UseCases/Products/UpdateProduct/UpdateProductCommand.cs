using Common.Application.Messaging;
using Modules.Catalog.Domain.ValueObjects;

namespace Modules.Catalog.Application.UseCases.Products.UpdateProduct;

public record UpdateProductCommand(
    Guid ProductId,
    LocalizedText ProductNames,
    LocalizedText LongDescriptions,
    LocalizedText ShortDescriptions,
    WeightUnit? WeightUnit,
    DimensionUnit? DimensionUnit,
    List<string>? Tags) : ICommand<Guid>;
