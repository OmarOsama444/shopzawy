using FluentValidation;
using MassTransit.SagaStateMachine;
using Modules.Common.Application.Clock;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Common.Domain.Exceptions;
using Modules.Users.Domain;
using Modules.Users.Domain.Entities;
using Modules.Users.Domain.Exceptions;
using Modules.Users.Domain.Repositories;

namespace Modules.Users.Application.UseCases.VerifyEmail;

public record VerifyEmailCommand(string VerifyToken) : ICommand;

public sealed class VerifyEmailCommandHandler(
    IEmailVerificationTokenRepository emailVerificationTokenRepository,
    IUserRepository userRepository,
    IUnverifiedUserRepository unverifiedUserRepository,
    IUnitOfWork unitOfWork,
    IDateTimeProvider dateTimeProvider) : ICommandHandler<VerifyEmailCommand>
{
    public async Task<Result> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
    {
        EmailVerificationToken? emailVerificationToken =
            await emailVerificationTokenRepository.GetByIdAsync(request.VerifyToken);
        if (emailVerificationToken is null)
            return new TokenNotFound(request.VerifyToken);
        if (emailVerificationToken.ExpiresOnUtc < dateTimeProvider.UtcNow)
            return new TokenExpired(request.VerifyToken);
        UnverifiedUser? unverifiedUser = await unverifiedUserRepository.GetByIdAsync(emailVerificationToken.UserId);
        if (unverifiedUser is null)
            return new UserNotFound(emailVerificationToken.UserId);
        if ((await userRepository.GetUserByEmail(unverifiedUser.Email)) is not null)
            return new UserAlreadyEmailVerified(unverifiedUser.Email);
        var user = User.Create(
            unverifiedUser.FirstName,
            unverifiedUser.LastName,
            unverifiedUser.HashedPassword,
            unverifiedUser.Role,
            unverifiedUser.Email
        );
        userRepository.Add(user);
        await unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}

internal sealed class VerifyEmailCommandValidator : AbstractValidator<VerifyEmailCommand>
{
    public VerifyEmailCommandValidator()
    {
        RuleFor(x => x.VerifyToken).NotEmpty().Length(36);
    }
}