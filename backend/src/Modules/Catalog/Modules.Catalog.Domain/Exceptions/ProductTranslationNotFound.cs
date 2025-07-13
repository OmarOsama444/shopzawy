using Common.Domain.Exceptions;
using Common.Domain.ValueObjects;

namespace Modules.Catalog.Domain.Exceptions;

public class ProductTranslationNotFound : NotFoundException
{
    public ProductTranslationNotFound
        (Guid ProductId, Language langCode) :
        base("Product.Translation.NotFound", $"product translation for product with id {ProductId} and lang code {langCode}")
    {
    }
}
