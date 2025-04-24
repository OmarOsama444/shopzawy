using Modules.Common.Domain.Exceptions;

namespace Modules.Orders.Domain.Exceptions;

public class ProductItemNotFoundException : NotFoundException
{
    public ProductItemNotFoundException(Guid Id) : base(
        "Product.Item.NotFound",
        $"Product with id {Id} not found")
    {
    }

}
