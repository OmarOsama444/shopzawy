using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Users.Application.Repositories;
using Modules.Users.Domain.Entities;
using Modules.Users.Domain.Exceptions;

namespace Modules.Users.Application.UseCases.Permissions.CreatePermission;

public class CreatePermissionCommandHandler(
    IPermissionRepository permissionRepository, IUnitOfWork unitOfWork) : ICommandHandler<CreatePermissionCommand, string>
{
    public async Task<Result<string>> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
    {
        if (await permissionRepository.GetByName(request.Name) is not null)
            return new PermissionNameConflict(request.Name);
        var permission = Permission.Create(request.Name, request.Active, request.Module);
        permissionRepository.Add(permission);
        await unitOfWork.SaveChangesAsync();
        return permission.Name;
    }
}
