using Common.Application.Messaging;
using Common.Domain;
using Modules.Catalog.Application.Abstractions;
using Modules.Catalog.Application.Repositories;
using Modules.Catalog.Domain.Entities;
using Modules.Catalog.Domain.Exceptions;

namespace Modules.Catalog.Application.UseCases.Vendors.UpdateVendor;

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
