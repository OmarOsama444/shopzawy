using Common.Application.Validators;
using FluentValidation;

namespace Modules.Orders.Application.UseCases.Vendors.UpdateVendor;

internal class UpdateVendorCommandValidator : AbstractValidator<UpdateVendorCommand>
{
    public UpdateVendorCommandValidator()
    {
        RuleFor(v => v.Id).NotEmpty();
        RuleFor(x => x.PhoneNumber)
                .Must((request, phoneNumber) =>
                    new PhoneNumberValidator(request.CountryCode).Must(phoneNumber!))
                .WithMessage(PhoneNumberValidator.Message)
                .When(x => !string.IsNullOrEmpty(x.PhoneNumber));
        RuleFor(v => v.LogoUrl!)
            .Must(UrlValidator.Must)
            .WithMessage(UrlValidator.Message)
            .When(v => !string.IsNullOrEmpty(v.LogoUrl));
    }
}
