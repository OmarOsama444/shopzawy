using Common.Application.Messaging;
using Modules.Users.Application.UseCases.Dtos;

namespace Modules.Users.Application.UseCases.Auth.LoginUser;

public record LoginUserCommand(string Email, string Password) : IQuery<LoginResponse>;
