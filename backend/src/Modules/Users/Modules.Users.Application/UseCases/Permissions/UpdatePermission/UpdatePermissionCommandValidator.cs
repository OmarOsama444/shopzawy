using FluentValidation;

namespace Modules.Users.Application.UseCases.Permissions.UpdatePermission;

public class UpdatePermissionCommandValidator : AbstractValidator<UpdatePermissionCommand>
{
    public UpdatePermissionCommandValidator()
    {
        RuleFor(x => x.Module).MinimumLength(4).Must(s => s == s.ToLower())
    .WithMessage("The value must be all lowercase.").When(x => !string.IsNullOrEmpty(x.Module));
        RuleFor(x => x.Id).NotEmpty(); ;
    }
}

