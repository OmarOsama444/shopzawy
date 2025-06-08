using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Users.Application.UseCases.Dtos;
using Modules.Users.Domain.Entities;
using Modules.Users.Domain.Exceptions;
using Modules.Users.Domain.Repositories;

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
        Role? role = await roleRepository.GetByIdAsync(request.Id);
        if (role is null)
            return new RoleNotFound(request.Id);
        var permissions = (await permissionRepository.GetByRoleId(request.Id)).Select(
            x => new PermissionResponse(x.Id, x.Name, x.Active, x.Module)
        ).ToList();
        return new RoleDetailResponse(role.Id, role.Name, role.CreatedOnUtc, permissions);
    }
}
