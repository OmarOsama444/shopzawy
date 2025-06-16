using Modules.Common.Application.Messaging;

namespace Modules.Users.Application.UseCases.Users.CreateUser
{
    public sealed record CreateUserCommand(
        Guid GuestId,
        string FirstName,
        string LastName,
        string Password,
        string? Email,
        string? PhoneNumber,
        string? CountryCode) : ICommand<Guid>;
}

