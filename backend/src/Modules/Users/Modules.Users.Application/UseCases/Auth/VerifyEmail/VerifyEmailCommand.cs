using Common.Application.Messaging;

namespace Modules.Users.Application.UseCases.VerifyEmail;

public record VerifyEmailCommand(string Token) : ICommand;
