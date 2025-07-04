using Common.Application.Messaging;

namespace Modules.Users.Application.UseCases.Roles.CreateRole;

public record CreateRoleCommand(string Name) : ICommand<string>;
