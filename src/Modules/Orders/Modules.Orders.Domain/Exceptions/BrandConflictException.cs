using Modules.Common.Domain.Exceptions;

namespace Modules.Orders.Domain.Exceptions;

public class BrandConflictException : ConflictException
{
    public BrandConflictException(Guid id) : base("Brand.Conflict", $"Brand with id {id} already exists")
    {
    }

}
