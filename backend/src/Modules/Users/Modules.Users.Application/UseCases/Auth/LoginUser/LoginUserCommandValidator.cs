using Common.Application.Validators;
using FluentValidation;

namespace Modules.Users.Application.UseCases.Auth.LoginUser;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(x => x.Email).EmailAddress().When(x => !string.IsNullOrEmpty(x.Email));
        RuleFor(x => x.PhoneNumber)
            .Must((request, phoneNumber) =>
            {
                return new PhoneNumberValidator(request.CountryCode)
                    .Must(phoneNumber);
            })
            .WithMessage(PhoneNumberValidator.Message)
            .When(x => !string.IsNullOrEmpty(x.PhoneNumber));
        RuleFor(x => x)
            .Must(x => !string.IsNullOrWhiteSpace(x.Email) || !string.IsNullOrWhiteSpace(x.PhoneNumber))
            .WithMessage("Either email or phone number must be provided.");
        RuleFor(x => x.Password)
        .MinimumLength(12).WithMessage("Password must be at least 8 characters long.")
        .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
        .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
        .Matches(@"\d").WithMessage("Password must contain at least one number.")
        .Matches(@"[\W_]").WithMessage("Password must contain at least one special character.");
    }
}
