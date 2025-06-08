using FluentValidation;

namespace Modules.Users.Application.UseCases.Roles.GetRoleById;

internal class GetRoleByIdQueryValidator : AbstractValidator<GetRoleByIdQuery>
{
    public GetRoleByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}