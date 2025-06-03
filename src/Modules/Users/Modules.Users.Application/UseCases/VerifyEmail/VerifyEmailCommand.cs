using FluentValidation;
using MassTransit.SagaStateMachine;
using Microsoft.AspNetCore.Server.HttpSys;
using Modules.Common.Application.Clock;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Common.Domain.Exceptions;
using Modules.Users.Domain;
using Modules.Users.Domain.Entities;
using Modules.Users.Domain.Exceptions;
using Modules.Users.Domain.Repositories;
using Modules.Users.Domain.ValueObjects;

namespace Modules.Users.Application.UseCases.VerifyEmail;

public record VerifyEmailCommand(string Token) : ICommand;

public sealed class VerifyEmailCommandHandler(
    IUserTokenRepository userTokenRepository,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork
) : ICommandHandler<VerifyEmailCommand>
{
    public async Task<Result> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
    {
        var token = await userTokenRepository.GetToken(request.Token, TokenType.Email);
        if (token is null)
            return new TokenNotFound(request.Token);
        var user = await userRepository.GetByIdAsync(token.UserId);
        if (user is null)
            return new UserNotFound(token.UserId);
        user.EmailConfirmed = true;
        await unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}

internal sealed class VerifyEmailCommandValidator : AbstractValidator<VerifyEmailCommand>
{
    public VerifyEmailCommandValidator()
    {
    }
}