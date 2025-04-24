using Modules.Common.Domain.Exceptions;

namespace Modules.Orders.Domain.Exceptions;

public class ProductNotFoundException : NotFoundException
{
    public ProductNotFoundException(Guid Id) : base(
        "Product.NotFound",
        $"Product with id {Id} not found")
    {
    }

}
