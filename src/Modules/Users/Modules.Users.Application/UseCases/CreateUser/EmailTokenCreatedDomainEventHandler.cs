using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain.Exceptions;
using Modules.Users.Application.Services;
using Modules.Users.Domain;
using Modules.Users.Domain.DomainEvents;
using Modules.Users.Domain.Entities;
using Modules.Users.Domain.Repositories;
using Modules.Users.Domain.ValueObjects;

namespace Modules.Users.Application.DomainEventHandlers;

public class EmailTokenCreatedDomainEventHandler(
    UserManager<User> userManager,
    IUserTokenRepository userTokenRepository,
    IEmailService emailService) : IDomainEventHandler<EmailTokenCreatedDomainEvent>
{
    public async Task Handle(EmailTokenCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        UserToken? token =
            await userTokenRepository
                .GetToken(notification.Token, TokenType.Email);
        if (token is null)
            throw new SkillHiveException("Email verification token is null");
        User? user = await userManager.Users.FirstOrDefaultAsync(x => x.Id == token.UserId);
        if (user is null)
            throw new SkillHiveException("User not found");
        await emailService.SendVerificationToken(user.FirstName, user.Email!, token.Value!);
    }
}
