using System.Data;
using FluentValidation;

namespace Modules.Users.Application.UseCases.Permissions.CreatePermission;

public class CreatePermissionCommandValidator : AbstractValidator<CreatePermissionCommand>
{
    public CreatePermissionCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(4);
        RuleFor(x => x.Active).NotEmpty();
        RuleFor(x => x.Module).MinimumLength(4).When(x => !string.IsNullOrEmpty(x.Module));
    }
}
