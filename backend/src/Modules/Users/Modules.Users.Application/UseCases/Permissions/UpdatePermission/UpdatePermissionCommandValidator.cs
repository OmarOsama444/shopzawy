using FluentValidation;

namespace Modules.Users.Application.UseCases.Permissions.UpdatePermission;

public class UpdatePermissionCommandValidator : AbstractValidator<UpdatePermissionCommand>
{
    public UpdatePermissionCommandValidator()
    {
        RuleFor(x => x.Module).MinimumLength(4).When(x => !string.IsNullOrEmpty(x.Module));
        RuleFor(x => x.Name).MinimumLength(4).When(x => !string.IsNullOrEmpty(x.Name));
    }
}

