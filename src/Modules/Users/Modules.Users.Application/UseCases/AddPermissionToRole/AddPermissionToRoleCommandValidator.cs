using FluentValidation;

namespace Modules.Users.Application.UseCases.AddPermissionToRole;

internal class AddPermissionToRoleCommandValidator : AbstractValidator<AddPermissionToRoleCommand>
{
    public AddPermissionToRoleCommandValidator()
    {
        RuleFor(x => x.RoleName).NotEmpty().MinimumLength(4);
        RuleFor(x => x.PermissionName).NotEmpty().MinimumLength(4);
    }
}
