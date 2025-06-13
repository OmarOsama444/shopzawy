using Modules.Users.Domain.Entities;

namespace Modules.Users.Application.UseCases.Dtos;

public record RoleDetailResponse(
    string Name,
    DateTime CreatedOnUtc,
    ICollection<PermissionResponse> Permissions);
