using Common.Domain.Exceptions;

namespace Modules.Orders.Domain.Exceptions;

public class ProductItemConflictException : ConflictException
{
    public ProductItemConflictException(string sku) : base(
        "Product.Item.Conflict",
        $"Product Item with this Sku {sku} already exists"
    )
    {
    }
}
