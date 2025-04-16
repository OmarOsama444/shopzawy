using System.Security.Cryptography.X509Certificates;
using FluentValidation;
using Modules.Users.Domain.ValueObjects;


namespace Modules.Users.Application.UseCases.CreateUser
{
    internal class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.FirstName)
               .NotEmpty().WithMessage("First name is required.")
               .MinimumLength(2).WithMessage("First name must be at least 3 characters long.")
               .MaximumLength(20).WithMessage("First name must not exceed 20 characters.");
            RuleFor(x => x.LastName)
                .MaximumLength(20).WithMessage("Last name must not exceed 20 characters.")
                .MinimumLength(2).WithMessage("Last name must be at least 3 characters long.")
                .When(x => !string.IsNullOrEmpty(x.LastName));
            RuleFor(x => x.Email)
                .EmailAddress()
                .When(x => !string.IsNullOrEmpty(x.Email));
            RuleFor(x => x.PhoneNumber)
                .Matches(@"^(?:\+20|0)(10|11|12|15)\d{8}$")
                .WithMessage("Invalid Egyptian phone number. It must start with +20 or 0, followed by 10, 11, 12, or 15, and contain 8 more digits.")
                .When(x => !string.IsNullOrEmpty(x.PhoneNumber));
            RuleFor(x => x.Role).NotEmpty().Must(role => UserRole.IsValidRole(role)).WithMessage("Invalid role");
            RuleFor(x => x)
                .Must(x => !string.IsNullOrEmpty(x.Email) || !string.IsNullOrEmpty(x.PhoneNumber))
                .WithMessage("You must provide at least an email or a phone number.");
        }
    }

}

