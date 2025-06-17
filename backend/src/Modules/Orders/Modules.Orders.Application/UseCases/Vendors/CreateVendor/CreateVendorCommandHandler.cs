using Common.Application.Messaging;
using Common.Domain;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Application.Repositories;
using Modules.Orders.Application.UseCases.CreateVendor;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Exceptions;

namespace Modules.Orders.Application.UseCases.Vendors.CreateVendor;

public class CreateVendorCommandHandler(IVendorRepository vendorRepository, IUnitOfWork unitOfWork) : ICommandHandler<CreateVendorCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateVendorCommand request, CancellationToken cancellationToken)
    {
        // TODO validate the uniqueness of the email and the phonenumebr conflict
        Vendor? vendor = await vendorRepository.GetVendorByEmail(request.email);
        if (vendor is not null)
            return new VendorConflictException(request.email, "Email");
        vendor = await vendorRepository.GetVendorByPhone(request.phoneNumber);
        if (vendor is not null)
            return new VendorConflictException(request.phoneNumber, "Phone");
        vendor = Vendor.Create(
            request.vendorName,
            request.email,
            request.phoneNumber,
            request.address,
            request.logoUrl,
            request.shippingZoneName,
            request.description,
            request.active);
        vendorRepository.Add(vendor);
        await unitOfWork.SaveChangesAsync();
        return vendor.Id;
    }
}
