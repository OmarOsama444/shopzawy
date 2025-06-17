using Common.Domain.Exceptions;

namespace Modules.Orders.Domain.Exceptions;

public class VendorConflictException : ConflictException
{
    public VendorConflictException(string field, string filedname) : base($"Vendor.Conflict.{filedname}", $"Vendor with {filedname} {field} already exists")
    {
    }
}