using Modules.Users.Domain.Entities;

namespace Modules.Users.Application.UseCases.Dtos;

public record RoleDetailResponse(
    Guid Id,
    string Name,
    DateTime CreatedOnUtc,
    ICollection<PermissionResponse> Permissions);
