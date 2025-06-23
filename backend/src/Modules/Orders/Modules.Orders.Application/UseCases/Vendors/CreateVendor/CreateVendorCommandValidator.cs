using Common.Application.Validators;
using FluentValidation;
using Modules.Orders.Application.UseCases.CreateVendor;

namespace Modules.Orders.Application.UseCases.Vendors.CreateVendor;

internal class CreateVendorCommandValidator : AbstractValidator<CreateVendorCommand>
{
    public CreateVendorCommandValidator()
    {
        var phoneValidator = new PhoneNumberValidator("EG");
        RuleFor(v => v.VendorName).NotEmpty().MinimumLength(3);
        RuleFor(v => v.Email).NotEmpty().EmailAddress();
        RuleFor(v => v.PhoneNumber).NotEmpty().Must(phoneValidator.Must).WithMessage(PhoneNumberValidator.Message)
        .WithMessage(PhoneNumberValidator.Message);
        RuleFor(v => v.Address).NotEmpty();
        RuleFor(v => v.Description).NotEmpty().MinimumLength(3);
        RuleFor(v => v.LogoUrl).Must(UrlValidator.Must).WithMessage(UrlValidator.Message);
    }
}