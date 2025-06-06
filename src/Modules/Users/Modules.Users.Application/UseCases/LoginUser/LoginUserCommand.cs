using Modules.Common.Application.Messaging;
using Modules.Users.Application.UseCases.Dtos;

namespace Modules.Users.Application.UseCases.LoginUser;

public record LoginUserCommand(string Email, string PhoneNumber, string CountryCode, string Password) : IQuery<LoginResponse>;
