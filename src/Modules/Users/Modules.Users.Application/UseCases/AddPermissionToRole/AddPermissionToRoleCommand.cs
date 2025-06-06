using Modules.Common.Application.Messaging;

namespace Modules.Users.Application.UseCases.AddPermissionToRole;

public record AddPermissionToRoleCommand(string RoleName, string PermissionName) : ICommand;
