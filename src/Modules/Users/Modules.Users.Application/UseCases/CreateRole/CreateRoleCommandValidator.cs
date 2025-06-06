using FluentValidation;

namespace Modules.Users.Application.UseCases.CreateRole;

internal class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator()
    {
        RuleFor(x => x.RoleName).NotEmpty().MinimumLength(3);
    }
}