using Common.Application.Messaging;
using Common.Domain;
using Modules.Catalog.Application.Abstractions;
using Modules.Catalog.Application.Repositories;
using Modules.Catalog.Domain.Entities;
using Modules.Catalog.Domain.Exceptions;

namespace Modules.Catalog.Application.UseCases.Vendors.CreateVendor;

public class CreateVendorCommandHandler(IVendorRepository vendorRepository, IUnitOfWork unitOfWork) : ICommandHandler<CreateVendorCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateVendorCommand request, CancellationToken cancellationToken)
    {
        // TODO validate the uniqueness of the email and the phonenumebr conflict
        Vendor? vendor = await vendorRepository.GetVendorByEmail(request.Email);
        if (vendor is not null)
            return new VendorConflictException(request.Email, "Email");
        vendor = await vendorRepository.GetVendorByPhone(request.PhoneNumber);
        if (vendor is not null)
            return new VendorConflictException(request.PhoneNumber, "Phone");
        vendor = Vendor.Create(
            request.VendorName,
            request.Email,
            request.PhoneNumber,
            request.Address,
            request.LogoUrl,
            request.ShippingZoneName,
            request.Description,
            request.Active);
        vendorRepository.Add(vendor);
        await unitOfWork.SaveChangesAsync();
        return vendor.Id;
    }
}
