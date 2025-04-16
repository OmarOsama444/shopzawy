using FluentValidation;
using Modules.Common.Application.Messaging;
using Modules.Common.Application.Validators;
using Modules.Common.Domain;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Exceptions;
using Modules.Orders.Domain.Repositories;

namespace Modules.Orders.Application.UseCases.CreateVendor;

public record CreateVendorCommand(
    string vendorName,
    string email,
    string phoneNumber,
    string address,
    string logoUrl,
    string shippingZoneName,
    string description,
    bool? active) : ICommand<Guid>;

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

internal class CreateVendorCommandValidator : AbstractValidator<CreateVendorCommand>
{
    public CreateVendorCommandValidator()
    {
        var phoneValidator = new PhoneNumberValidator("EG", "Egypt");
        RuleFor(v => v.vendorName).NotEmpty().MinimumLength(3);
        RuleFor(v => v.email).NotEmpty().EmailAddress();
        RuleFor(v => v.phoneNumber).NotEmpty().Must(phoneValidator.Must).WithMessage(phoneValidator.Message)
        .WithMessage(phoneValidator.Message);
        RuleFor(v => v.address).NotEmpty();
        RuleFor(v => v.description).NotEmpty().MinimumLength(3);
        RuleFor(v => v.logoUrl).Must(UrlValidator.Must).WithMessage(UrlValidator.Message);
    }
}