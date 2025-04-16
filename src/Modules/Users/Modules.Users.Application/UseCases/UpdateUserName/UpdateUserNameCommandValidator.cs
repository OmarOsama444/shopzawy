
using FluentValidation;

namespace Modules.Users.Application.UseCases;

internal class UpdateUserNameCommandValidator : AbstractValidator<UpdateUserNameCommand>
{
    public UpdateUserNameCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.FirstName)
              .NotEmpty().WithMessage("Username is required.")
              .MinimumLength(3).WithMessage("Username must be at least 3 characters long.")
              .MaximumLength(20).WithMessage("Username must not exceed 20 characters.")
              .Matches(@"^[a-zA-Z]").WithMessage("Username must start with a letter.")
              .Matches(@"^[a-zA-Z0-9._]+$").WithMessage("Username can only contain letters, numbers, dots, and underscores.");
        RuleFor(x => x.LastName)
              .NotEmpty().WithMessage("Username is required.")
              .MinimumLength(3).WithMessage("Username must be at least 3 characters long.")
              .MaximumLength(20).WithMessage("Username must not exceed 20 characters.")
              .Matches(@"^[a-zA-Z]").WithMessage("Username must start with a letter.")
              .Matches(@"^[a-zA-Z0-9._]+$").WithMessage("Username can only contain letters, numbers, dots, and underscores.");
    }
}