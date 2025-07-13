using Common.Domain.Exceptions;

namespace Modules.Catalog.Domain.Exceptions;

public class BrandConflictException : ConflictException
{
    public BrandConflictException(Guid id) : base("Brand.Conflict", $"Brand with id {id} already exists")
    {
    }

}
