using Modules.Common.Application.Messaging;

namespace Modules.Users.Application.UseCases.CreatePermission;

public record CreatePermissionCommand(string PermissionName) : ICommand<string>;
