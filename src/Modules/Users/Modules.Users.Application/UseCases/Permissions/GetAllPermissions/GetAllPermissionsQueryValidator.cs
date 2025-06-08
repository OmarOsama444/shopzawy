using FluentValidation;

namespace Modules.Users.Application.UseCases.Permissions.GetAllPermissions;

internal class GetAllPermissionsQueryValidator : AbstractValidator<GetAllPermissionsQuery>
{
    public GetAllPermissionsQueryValidator()
    {

    }
}
