using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Users.Domain.Entities;
using Modules.Users.Domain.Exceptions;
using Modules.Users.Domain.Repositories;

namespace Modules.Users.Application.UseCases.AddPermissionToRole;

public class AddPermissionToRoleCommandHandler(
    IRoleRepository roleRepository,
    IPermissionRepository permissionRepository,
    IRolePermissionRepository rolePermissionRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<AddPermissionToRoleCommand>
{
    public async Task<Result> Handle(AddPermissionToRoleCommand request, CancellationToken cancellationToken)
    {
        Role? role = await roleRepository.GetByIdAsync(request.RoleName);
        if (role is null)
            return new RoleNotFound(request.RoleName);
        Permission? permission = await permissionRepository
            .GetByIdAsync(request.PermissionName);
        if (permission is null)
            return new PermissionNotFound(request.PermissionName);
        var rolePermission = new RolePermission() { RoleName = role.Name, PermissionName = permission.Value };
        rolePermissionRepository.Add(rolePermission);
        await unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}
