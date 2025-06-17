using Common.Application.Messaging;

namespace Modules.Users.Application.UseCases.Roles.UpdateRolePermissions;

public record UpdateRolePermissionsCommand(string Id, ICollection<string> AddPermissions, ICollection<string> RemovePermissions) : ICommand;
