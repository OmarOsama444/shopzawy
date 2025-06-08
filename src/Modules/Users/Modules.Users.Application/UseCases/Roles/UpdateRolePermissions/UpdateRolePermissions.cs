using Modules.Common.Application.Messaging;

namespace Modules.Users.Application.UseCases.Roles.UpdateRolePermissions;

public record UpdateRolePermissionsCommand(Guid Id, ICollection<Guid> AddPermissions, ICollection<Guid> RemovePermissions) : ICommand;
