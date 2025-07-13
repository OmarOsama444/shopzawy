using Common.Application.Messaging;
using Common.Domain.ValueObjects;
using Modules.Catalog.Application.Services.Dtos;
using Modules.Catalog.Domain.ValueObjects;

namespace Modules.Catalog.Application.UseCases.Products.CreateProduct;

public record CreateProductCommand(
    IDictionary<Language, string> ProductNames,
    IDictionary<Language, string> LongDescriptions,
    IDictionary<Language, string> ShortDescriptions,
    List<string> Tags,
    WeightUnit WeightUnit,
    DimensionUnit DimensionUnit,
    Guid VendorId,
    Guid BrandId,
    Guid CategoryId,
    ICollection<ProductItemDto> ProductItems) : ICommand<Guid>;
