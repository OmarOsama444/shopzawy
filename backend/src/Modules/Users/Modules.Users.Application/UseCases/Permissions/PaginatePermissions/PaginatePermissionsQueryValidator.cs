using FluentValidation;

namespace Modules.Users.Application.UseCases.Permissions.PaginatePermissions;

internal class PaginatePermissionsQueryValidator : AbstractValidator<PaginatePermissionsQuery>
{
    public PaginatePermissionsQueryValidator()
    {
        RuleFor(x => x.PageNumber).GreaterThan(0);
        RuleFor(x => x.PageSize).InclusiveBetween(1, 50);
    }
}