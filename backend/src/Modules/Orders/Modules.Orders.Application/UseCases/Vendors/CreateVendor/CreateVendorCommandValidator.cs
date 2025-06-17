using Common.Application.Validators;
using FluentValidation;
using Modules.Orders.Application.UseCases.CreateVendor;

namespace Modules.Orders.Application.UseCases.Vendors.CreateVendor;

internal class CreateVendorCommandValidator : AbstractValidator<CreateVendorCommand>
{
    public CreateVendorCommandValidator()
    {
        var phoneValidator = new PhoneNumberValidator("EG");
        RuleFor(v => v.vendorName).NotEmpty().MinimumLength(3);
        RuleFor(v => v.email).NotEmpty().EmailAddress();
        RuleFor(v => v.phoneNumber).NotEmpty().Must(phoneValidator.Must).WithMessage(PhoneNumberValidator.Message)
        .WithMessage(PhoneNumberValidator.Message);
        RuleFor(v => v.address).NotEmpty();
        RuleFor(v => v.description).NotEmpty().MinimumLength(3);
        RuleFor(v => v.logoUrl).Must(UrlValidator.Must).WithMessage(UrlValidator.Message);
    }
}