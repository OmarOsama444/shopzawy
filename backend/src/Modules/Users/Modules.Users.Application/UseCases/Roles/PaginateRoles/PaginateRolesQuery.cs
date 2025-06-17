using Common.Application.Messaging;
using Modules.Users.Application.UseCases.Dtos;

namespace Modules.Users.Application.UseCases.Roles.PaginateRoles;

public record PaginateRolesQuery(
    int PageNumber,
    int PageSize,
    string? Name) : IQuery<PaginationResponse<RoleResponse>>;
