using Common.Domain.ValueObjects;

namespace Modules.Catalog.Application.UseCases.Products.UpdateProduct;

public record LocalizedText(IDictionary<Language, string> translations);
