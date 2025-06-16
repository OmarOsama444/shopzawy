using FluentValidation;

namespace Modules.Users.Application.UseCases.Roles.PaginateRoles;

internal class PaginateRolesQueryValidator : AbstractValidator<PaginateRolesQuery>
{
    public PaginateRolesQueryValidator()
    {
        RuleFor(x => x.PageNumber).GreaterThan(0);
        RuleFor(x => x.PageSize).InclusiveBetween(1, 50);
    }
}