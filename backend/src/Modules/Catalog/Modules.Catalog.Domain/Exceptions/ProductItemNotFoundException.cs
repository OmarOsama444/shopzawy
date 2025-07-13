using Common.Domain.Exceptions;

namespace Modules.Catalog.Domain.Exceptions;

public class ProductItemNotFoundException : NotFoundException
{
    public ProductItemNotFoundException(Guid Id) : base(
        "Product.Item.NotFound",
        $"Product with id {Id} not found")
    {
    }

}
