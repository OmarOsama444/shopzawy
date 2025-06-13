using FluentValidation;

namespace Modules.Users.Application.UseCases.Users.UpdateUserRoles;

internal class UpdateUserRolesCommandValidator : AbstractValidator<UpdateUserRolesCommand>
{
    public UpdateUserRolesCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}