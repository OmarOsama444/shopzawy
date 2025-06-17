using Common.Application.Messaging;
using Modules.Users.Application.UseCases.Dtos;

namespace Modules.Users.Application.UseCases.Auth.LoginGuestUser;

public record LoginGuestUserCommand() : ICommand<LoginResponse>;
