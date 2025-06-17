using Common.Application.Messaging;
using Modules.Users.Application.UseCases.Dtos;

namespace Modules.Users.Application.UseCases.Permissions.PaginatePermissions;

public record PaginatePermissionsQuery(
    int PageNumber,
    int PageSize,
    string? Name) : IQuery<PaginationResponse<PermissionResponse>>;
