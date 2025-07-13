using Common.Application.Messaging;

namespace Modules.Users.Application.UseCases.Users.CreateUser
{
    public sealed record CreateUserCommand(
        string FirstName,
        string LastName,
        string Password,
        string Email) : ICommand<Guid>;
}

