using System.ComponentModel;
using Microsoft.AspNetCore.Http;
using Modules.Common.Application.Messaging;

namespace Modules.Users.Application.UseCases.Users.UpdateUserRoles;

public record UpdateUserRolesCommand(
    Guid Id,
    ICollection<string> AddRoles,
    ICollection<string> RemoveRoles) : ICommand;
