using Common.Application.Messaging;
using Common.Domain.Exceptions;
using Modules.Users.Application.Repositories;
using Modules.Users.Application.Services;
using Modules.Users.Domain.DomainEvents;
using Modules.Users.Domain.Entities;
using Modules.Users.Domain.ValueObjects;

namespace Modules.Users.Application.UseCases.Users.Projections;

public class EmailTokenCreatedDomainEventHandler(
    ITokenRepository tokenRepository,
    IUserRepository userRepository,
    IEmailService emailService) : IDomainEventHandler<EmailTokenCreatedDomainEvent>
{
    public async Task Handle(EmailTokenCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        Token? token =
            await tokenRepository
            .GetByTokenTypeAndValue(
                notification.Token,
                TokenType.Email
            );

        if (token is null)
            throw new SkillHiveException("Email verification token is null");
        User? user = await userRepository.GetByIdAsync(token.UserId);
        if (user is null)
            throw new SkillHiveException("User not found");
        await emailService.SendVerificationToken(user.FirstName, user.Email!, token.Value!);
    }
}
