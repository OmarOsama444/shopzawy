using FluentValidation;

namespace Modules.Users.Application.UseCases.Roles.UpdateRolePermissions;

internal class UpdateRolePermissionsCommandValidator : AbstractValidator<UpdateRolePermissionsCommand>
{
    public UpdateRolePermissionsCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
