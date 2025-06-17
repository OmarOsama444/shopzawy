using Common.Domain.Exceptions;

namespace Modules.Orders.Domain.Exceptions;

public class VendorNotFoundException : NotFoundException
{
    public VendorNotFoundException(Guid id) : base("Vendor.NotFound", $"Vendor with id {id} not found")
    {

    }
}
