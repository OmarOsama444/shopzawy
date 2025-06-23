using FluentValidation;

namespace Modules.Users.Application.UseCases.Roles.CreateRole;

internal class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(3).Must(s => s == s.ToLower())
        .WithMessage("The value must be all lowercase.");
    }
}