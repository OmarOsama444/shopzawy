using Modules.Common.Application.Messaging;

namespace Modules.Users.Application.UseCases.Permissions.UpdatePermission;

public record UpdatePermissionCommand(Guid Id, string? Name = null, string? Module = null, bool? Active = null) : ICommand<Guid>;

