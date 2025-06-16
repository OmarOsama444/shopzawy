using MassTransit.Initializers;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Users.Application.Repositories;
using Modules.Users.Application.UseCases.Dtos;

namespace Modules.Users.Application.UseCases.Permissions.GetAllPermissions;

public class GetAllPermissionsQueryHandler(
    IPermissionRepository permissionRepository
) : IQueryHandler<GetAllPermissionsQuery, ICollection<PermissionResponse>>
{
    public async Task<Result<ICollection<PermissionResponse>>> Handle(GetAllPermissionsQuery request, CancellationToken cancellationToken)
    {
        return
            (
                await permissionRepository
                    .GetAllAsync()
            )
            .Select(x => new PermissionResponse(x.Name, x.Active, x.Module ?? ""))
            .ToList();
    }
}
