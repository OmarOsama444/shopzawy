using Common.Domain.ValueObjects;

namespace Modules.Orders.Application.UseCases.Products.UpdateProduct;

public record LocalizedText(IDictionary<Language, string> translations);
