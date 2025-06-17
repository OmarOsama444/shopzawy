using Common.Domain.Exceptions;
using Common.Domain.ValueObjects;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Domain.Exceptions;

public class ProductTranslationNotFound : NotFoundException
{
    public ProductTranslationNotFound
        (Guid ProductId, Language langCode) :
        base("Product.Translation.NotFound", $"product translation for product with id {ProductId} and lang code {langCode}")
    {
    }
}
