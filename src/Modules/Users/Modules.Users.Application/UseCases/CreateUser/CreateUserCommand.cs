using Modules.Common.Application.Messaging;
using Modules.Users.Domain.Exceptions;

namespace Modules.Users.Application.UseCases.CreateUser
{
    public sealed record CreateUserCommand(
        string FirstName,
        string LastName,
        string Password,
        string? Email,
        string? PhoneNumber,
        string? CountryCode) : ICommand<Guid>;
}

