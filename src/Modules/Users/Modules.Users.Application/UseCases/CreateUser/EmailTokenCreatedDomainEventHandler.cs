using Modules.Common.Application.Messaging;
using Modules.Common.Domain.Exceptions;
using Modules.Users.Application.Services;
using Modules.Users.Domain;
using Modules.Users.Domain.DomainEvents;
using Modules.Users.Domain.Entities;
using Modules.Users.Domain.Repositories;

namespace Modules.Users.Application.DomainEventHandlers;

public class EmailTokenCreatedDomainEventHandler(
    IEmailVerificationTokenRepository emailVerificationTokenRepository,
    IUnverifiedUserRepository unverifiedUserRepository,
    IEmailService emailService) : IDomainEventHandler<EmailTokenCreatedDomainEvent>
{
    public async Task Handle(EmailTokenCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        EmailVerificationToken? token = await emailVerificationTokenRepository.GetByIdAsync(notification.Token);
        if (token is null)
            throw new SkillHiveException("Email verification token is null");
        UnverifiedUser? user = await unverifiedUserRepository.GetByIdAsync(token.UserId);
        if (user is null)
            throw new SkillHiveException("User not found");
        await emailService.SendVerificationToken(user.FirstName, user.Email, token.Id);
    }
}
