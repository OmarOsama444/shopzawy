using Modules.Common.Application.Messaging;

namespace Modules.Users.Application.UseCases.Permissions.CreatePermission;

public record CreatePermissionCommand(
    string Name,
    bool Active = true,
    string? Module = null) : ICommand<string>;
