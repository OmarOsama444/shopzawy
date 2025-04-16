using Modules.Common.Application.Messaging;
using Modules.Users.Domain.Exceptions;

namespace Modules.Users.Application.UseCases.CreateUser
{
    public sealed record CreateUserCommand(string FirstName, string LastName, string HashedPassword, string Role, string Email, string PhoneNumber) : ICommand<Guid>;
}

