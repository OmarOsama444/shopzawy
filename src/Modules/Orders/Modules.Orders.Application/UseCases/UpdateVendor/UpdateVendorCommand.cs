using FluentValidation;
using Modules.Common.Application.Messaging;
using Modules.Common.Application.Validators;
using Modules.Common.Domain;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Exceptions;
using Modules.Orders.Domain.Repositories;

namespace Modules.Orders.Application.UseCases.UpdateVendor;

public record UpdateVendorCommand(Guid Id, string? VendorName, string? Description, string? Email, string? PhoneNumber, string? Address, string? LogoUrl, string? ShipingZoneName, bool? active) : ICommand;

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
            active: request.active);
        vendorRepository.Update(vendor);
        await unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}

internal class UpdateVendorCommandValidator : AbstractValidator<UpdateVendorCommand>
{
    public UpdateVendorCommandValidator()
    {
        var phoneValidator = new PhoneNumberValidator("EG", "Egypt");
        RuleFor(v => v.Id).NotEmpty();
        RuleFor(v => v.PhoneNumber!).Must(phoneValidator.Must).WithMessage(phoneValidator.Message).When(v => !String.IsNullOrEmpty(v.PhoneNumber));
        RuleFor(v => v.LogoUrl!)
            .Must(UrlValidator.Must)
            .WithMessage(UrlValidator.Message)
            .When(v => !String.IsNullOrEmpty(v.LogoUrl));
    }
}
