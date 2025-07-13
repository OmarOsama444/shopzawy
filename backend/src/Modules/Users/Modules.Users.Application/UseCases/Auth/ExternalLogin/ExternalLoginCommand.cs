using Common.Application.Messaging;
using Modules.Users.Application.UseCases.Dtos;
using Modules.Users.Domain.ValueObjects;

namespace Modules.Users.Application.UseCases.Auth.ExternalLogin;

public record ExternalLoginCommand(string ProviderToken, string? Email, string? FirstName, string? LastName, TokenType provider) : ICommand<LoginResponse>;
