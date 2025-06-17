using Common.Application.Messaging;
using Modules.Users.Application.UseCases.Dtos;

namespace Modules.Users.Application.UseCases.Permissions.GetAllPermissions;

public record GetAllPermissionsQuery() : IQuery<ICollection<PermissionResponse>>;
