using Modules.Common.Domain.Exceptions;

namespace Modules.Orders.Domain.Exceptions;

public class BrandConflictException : ConflictException
{
    public BrandConflictException(string name) : base("Brand.Conflict", $"Brand with name {name} already exists")
    {
    }

}

public class BrandNotFoundException : NotFoundException
{
    public BrandNotFoundException(string name) : base("Brand.Notfound",
     $"Brand with name {name} not found")
    {
    }

}