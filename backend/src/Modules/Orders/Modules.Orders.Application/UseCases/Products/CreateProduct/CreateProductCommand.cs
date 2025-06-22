using Common.Application.Messaging;
using Common.Domain.ValueObjects;
using Modules.Orders.Application.Services.Dtos;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Application.UseCases.Products.CreateProduct;

public record CreateProductCommand(
    IDictionary<Language, string> ProductNames,
    IDictionary<Language, string> LongDescriptions,
    IDictionary<Language, string> ShortDescriptions,
    ICollection<string> Tags,
    WeightUnit WeightUnit,
    DimensionUnit DimensionUnit,
    Guid VendorId,
    Guid BrandId,
    Guid CategoryId,
    ICollection<ProductItemDto> ProductItems) : ICommand<Guid>;
