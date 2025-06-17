using System.ComponentModel;
using Common.Application.Messaging;
using Microsoft.AspNetCore.Http;

namespace Modules.Users.Application.UseCases.Users.UpdateUserRoles;

public record UpdateUserRolesCommand(
    Guid Id,
    ICollection<string> AddRoles,
    ICollection<string> RemoveRoles) : ICommand;
