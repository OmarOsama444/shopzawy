using Modules.Common.Application.Messaging;

namespace Modules.Users.Application.UseCases;

public record UpdateUserNameCommand(Guid UserId, string FirstName, string LastName) : ICommand<Guid>;
