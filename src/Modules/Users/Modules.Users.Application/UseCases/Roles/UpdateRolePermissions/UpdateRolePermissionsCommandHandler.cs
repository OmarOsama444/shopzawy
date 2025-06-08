using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Users.Domain.Entities;
using Modules.Users.Domain.Exceptions;
using Modules.Users.Domain.Repositories;

namespace Modules.Users.Application.UseCases.Roles.UpdateRolePermissions;

public class UpdateRolePermissionsCommandHandler(
    IRoleRepository roleRepository,
    IPermissionRepository permissionRepository,
    IRolePermissionRepository rolePermissionRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateRolePermissionsCommand>
{
    public async Task<Result> Handle(UpdateRolePermissionsCommand request, CancellationToken cancellationToken)
    {
        Role? role = await roleRepository.GetByIdAsync(request.Id);
        if (role is null)
            return new RoleNotFound(request.Id);
        foreach (Guid permissionId in request.AddPermissions)
        {
            Permission? permission = await permissionRepository
                .GetByIdAsync(permissionId);
            if (permission is null)
                return new PermissionNotFound(permissionId);
            var rolePermission = new RolePermission()
            {
                RoleId = role.Id,
                PermissionId = permission.Id
            };
            rolePermissionRepository.Add(rolePermission);
        }
        foreach (Guid permissionId in request.RemovePermissions)
        {
            Permission? permission = await permissionRepository
                .GetByIdAsync(permissionId);
            if (permission is null)
                return new PermissionNotFound(permissionId);
            var rolePermission =
                await rolePermissionRepository
                    .GetByRoleAndPermissionId(role.Id, permissionId);
            if (rolePermission is not null)
                rolePermissionRepository.Remove(rolePermission);
        }
        await unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}
