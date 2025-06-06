using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Users.Domain.Entities;
using Modules.Users.Domain.Exceptions;
using Modules.Users.Domain.Repositories;

namespace Modules.Users.Application.UseCases.CreatePermission;

public class CreatePermissionCommandHandler(
    IPermissionRepository permissionRepository, IUnitOfWork unitOfWork) : ICommandHandler<CreatePermissionCommand, string>
{
    public async Task<Result<string>> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
    {
        if (await permissionRepository.GetByIdAsync(request.PermissionName) is not null)
            return new PermissionNameConflict(request.PermissionName);
        var permission = new Permission() { Value = request.PermissionName };
        permissionRepository.Add(permission);
        await unitOfWork.SaveChangesAsync();
        return permission.Value;
    }
}
