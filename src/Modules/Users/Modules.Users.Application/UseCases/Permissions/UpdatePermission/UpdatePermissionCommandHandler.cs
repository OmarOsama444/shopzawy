using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Users.Domain.Exceptions;
using Modules.Users.Domain.Repositories;

namespace Modules.Users.Application.UseCases.Permissions.UpdatePermission;

public class UpdatePermissionCommandHandler(
    IPermissionRepository permissionRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdatePermissionCommand, Guid>
{
    public async Task<Result<Guid>> Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
    {
        var permission = await permissionRepository.GetByIdAsync(request.Id);
        if (permission is null)
            return new PermissionNotFound(request.Id);
        permission.Update(request.Name, request.Active, request.Module);
        permissionRepository.Update(permission);
        await unitOfWork.SaveChangesAsync();
        return permission.Id;
    }
}

