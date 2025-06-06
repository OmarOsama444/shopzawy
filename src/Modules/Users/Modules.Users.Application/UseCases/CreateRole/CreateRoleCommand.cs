using Modules.Common.Application.Messaging;

namespace Modules.Users.Application.UseCases.CreateRole;

public record CreateRoleCommand(string RoleName) : ICommand<string>;
