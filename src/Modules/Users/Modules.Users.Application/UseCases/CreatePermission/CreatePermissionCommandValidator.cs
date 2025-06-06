using FluentValidation;

namespace Modules.Users.Application.UseCases.CreatePermission;

public class CreatePermissionCommandValidator : AbstractValidator<CreatePermissionCommand>
{
    public CreatePermissionCommandValidator()
    {
        RuleFor(x => x.PermissionName).NotEmpty().MinimumLength(4);
    }
}
