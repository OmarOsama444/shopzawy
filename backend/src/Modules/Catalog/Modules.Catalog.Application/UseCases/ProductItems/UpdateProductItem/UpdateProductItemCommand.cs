using System.Data;
using Common.Application.Messaging;

namespace Modules.Catalog.Application.UseCases.ProductItems.UpdateProductItem;

public record UpdateProductItemCommand(
    Guid ProductItemId,
    string? StockKeepingUnit,
    int? QuantityInStock,
    float? Price,
    float? Width,
    float? Length,
    float? Height,
    float? Weight,
    ICollection<string>? AddUrls,
    ICollection<string>? RemoveUrls
) : ICommand<Guid>;
