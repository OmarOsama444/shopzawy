using Modules.Common.Application.Messaging;

namespace Modules.Users.Application.UseCases.Roles.CreateRole;

public record CreateRoleCommand(string Name) : ICommand<Guid>;
