using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Users.Application.Repositories;
using Modules.Users.Application.UseCases.Dtos;

namespace Modules.Users.Application.UseCases.Permissions.PaginatePermissions;

public class PaginatePermissionsQueryHandler(
    IPermissionRepository permissionRepository
) : IQueryHandler<PaginatePermissionsQuery, PaginationResponse<PermissionResponse>>
{
    public async Task<Result<PaginationResponse<PermissionResponse>>> Handle(PaginatePermissionsQuery request, CancellationToken cancellationToken)
    {
        var data = (await permissionRepository.Paginate(request.PageSize, request.PageNumber, request.Name)).Select(x => new PermissionResponse(x.Name, x.Active, x.Module)).ToList();
        var count = await permissionRepository.Count(request.Name);
        return new PaginationResponse<PermissionResponse>(data, count, request.PageSize, request.PageNumber);
    }

}
