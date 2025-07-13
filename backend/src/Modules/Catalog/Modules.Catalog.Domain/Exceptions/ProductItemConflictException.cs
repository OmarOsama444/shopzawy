using Common.Domain.Exceptions;

namespace Modules.Catalog.Domain.Exceptions;

public class ProductItemConflictException : ConflictException
{
    public ProductItemConflictException(string sku) : base(
        "Product.Item.Conflict",
        $"Product Item with this Sku {sku} already exists"
    )
    {
    }
}
