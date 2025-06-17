using Common.Application.Messaging;
using Common.Domain;
using Modules.Users.Application.Repositories;
using Modules.Users.Domain.Entities;
using Modules.Users.Domain.Exceptions;

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
        foreach (string permissionId in request.AddPermissions)
        {
            Permission? permission = await permissionRepository
                .GetByIdAsync(permissionId);
            if (permission is null)
                return new PermissionNotFound(permissionId);
            var rolePermission = new RolePermission()
            {
                RoleId = role.Name,
                PermissionId = permission.Name
            };
            rolePermissionRepository.Add(rolePermission);
        }
        foreach (string permissionId in request.RemovePermissions)
        {
            Permission? permission = await permissionRepository
                .GetByIdAsync(permissionId);
            if (permission is null)
                return new PermissionNotFound(permissionId);
            var rolePermission =
                await rolePermissionRepository
                    .GetByRoleAndPermissionId(role.Name, permissionId);
            if (rolePermission is not null)
                rolePermissionRepository.Remove(rolePermission);
        }
        await unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}
