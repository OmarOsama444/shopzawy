using Common.Application.Messaging;

namespace Modules.Users.Application.UseCases.Permissions.UpdatePermission;

public record UpdatePermissionCommand(string Id, string? Name = null, string? Module = null, bool? Active = null) : ICommand<string>;

