using Common.Application.Validators;
using FluentValidation;


namespace Modules.Users.Application.UseCases.Users.CreateUser
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
            RuleFor(x => x.Password)
                .MinimumLength(12).WithMessage("Password must be at least 8 characters long.")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"\d").WithMessage("Password must contain at least one number.")
                .Matches(@"[\W_]").WithMessage("Password must contain at least one special character.");
        }
    }

}

