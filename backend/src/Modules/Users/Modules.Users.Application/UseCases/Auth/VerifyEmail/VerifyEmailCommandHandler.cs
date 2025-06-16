using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Users.Application.Repositories;
using Modules.Users.Domain.Exceptions;
using Modules.Users.Domain.ValueObjects;


namespace Modules.Users.Application.UseCases.VerifyEmail;

public sealed class VerifyEmailCommandHandler(
    ITokenRepository tokenRepository,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork
) : ICommandHandler<VerifyEmailCommand>
{
    public async Task<Result> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
    {
        var token = await
            tokenRepository
            .GetByTokenTypeAndValue(request.Token, TokenType.Email);
        if (token is null)
            return new TokenNotFound(request.Token);
        var user = await
            userRepository
            .GetByIdAsync(token.UserId);
        if (user is null)
            return new UserNotFound(token.UserId);
        if ((await
                userRepository
                .GetByConfirmedEmail(
                    user?.Email!
                )) is not null)
            return new UserConflictEmail(user?.Email!);
        user!.EmailConfirmed = true;
        await unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}
