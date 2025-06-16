using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Users.Application.Repositories;
using Modules.Users.Application.UseCases.Dtos;
using Modules.Users.Domain.Entities;
using Modules.Users.Domain.Exceptions;

namespace Modules.Users.Application.UseCases.Roles.GetRoleById;

public sealed class GetRoleByIdQueryHandler(
    IRoleRepository roleRepository,
    IPermissionRepository permissionRepository
) : IQueryHandler<GetRoleByIdQuery, RoleDetailResponse>
{
    public async Task<Result<RoleDetailResponse>> Handle(
        GetRoleByIdQuery request,
        CancellationToken cancellationToken)
    {
        Role? role = await roleRepository.GetByIdAsync(request.name);
        if (role is null)
            return new RoleNotFound(request.name);
        var permissions = (await permissionRepository.GetByRoleId(request.name)).Select(
            x => new PermissionResponse(x.Name, x.Active, x.Module)
        ).ToList();
        return new RoleDetailResponse(role.Name, role.CreatedOnUtc, permissions);
    }
}
