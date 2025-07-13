using Common.Domain.Exceptions;

namespace Modules.Catalog.Domain.Exceptions;

public class BrandNotFoundException : NotFoundException
{
    public BrandNotFoundException(Guid id) : base("Brand.Notfound",
     $"Brand with id {id} not found")
    {
    }

}
