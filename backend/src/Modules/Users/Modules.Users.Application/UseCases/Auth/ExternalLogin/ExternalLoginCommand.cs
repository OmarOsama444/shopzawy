using Common.Application.Messaging;
using Modules.Users.Application.UseCases.Dtos;

namespace Modules.Users.Application.UseCases.Auth.ExternalLogin;

public record ExternalLoginCommand(Guid GuestId, string Email, string FirstName, string? LastName) : ICommand<LoginResponse>;
