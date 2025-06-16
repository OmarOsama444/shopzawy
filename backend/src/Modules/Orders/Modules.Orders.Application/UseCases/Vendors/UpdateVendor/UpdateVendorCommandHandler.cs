using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Application.Repositories;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Exceptions;

namespace Modules.Orders.Application.UseCases.Vendors.UpdateVendor;

public class UpdateVendorCommandHandler(IVendorRepository vendorRepository, IUnitOfWork unitOfWork) : ICommandHandler<UpdateVendorCommand>
{
    public async Task<Result> Handle(UpdateVendorCommand request, CancellationToken cancellationToken)
    {
        Vendor? vendor = await vendorRepository.GetByIdAsync(request.Id);
        if (vendor is null)
            return new VendorNotFoundException(request.Id);
        vendor.Update(
            vendorName: request.VendorName,
            description: request.Description,
            email: request.Email,
            phoneNumber: request.PhoneNumber,
            address: request.Address,
            logoUrl: request.LogoUrl,
            shipingZoneName: request.ShipingZoneName,
            active: request.Active);
        vendorRepository.Update(vendor);
        _ = await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
