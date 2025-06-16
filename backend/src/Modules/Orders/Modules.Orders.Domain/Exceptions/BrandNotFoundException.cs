using Modules.Common.Domain.Exceptions;

namespace Modules.Orders.Domain.Exceptions;

public class BrandNotFoundException : NotFoundException
{
    public BrandNotFoundException(Guid id) : base("Brand.Notfound",
     $"Brand with id {id} not found")
    {
    }

}