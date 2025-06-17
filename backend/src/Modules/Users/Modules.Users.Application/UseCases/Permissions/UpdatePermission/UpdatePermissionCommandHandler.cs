using Common.Application.Messaging;
using Common.Domain;
using Modules.Users.Application.Repositories;
using Modules.Users.Domain.Exceptions;

namespace Modules.Users.Application.UseCases.Permissions.UpdatePermission;

public class UpdatePermissionCommandHandler(
    IPermissionRepository permissionRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdatePermissionCommand, string>
{
    public async Task<Result<string>> Handle(
        UpdatePermissionCommand request,
        CancellationToken cancellationToken)
    {
        var permission = await permissionRepository.GetByIdAsync(request.Id);
        if (permission is null)
            return new PermissionNotFound(request.Id);
        permission.Update(request.Name, request.Active, request.Module);
        permissionRepository.Update(permission);
        await unitOfWork.SaveChangesAsync();
        return permission.Name;
    }
}

