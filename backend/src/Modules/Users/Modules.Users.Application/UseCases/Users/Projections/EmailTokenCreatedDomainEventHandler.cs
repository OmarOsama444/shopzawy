using Common.Application.Messaging;
using Common.Domain.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using Modules.Users.Application.Repositories;
using Modules.Users.Application.Services;
using Modules.Users.Domain.DomainEvents;
using Modules.Users.Domain.Entities;
using Modules.Users.Domain.ValueObjects;

namespace Modules.Users.Application.UseCases.Users.Projections;

public class EmailTokenCreatedDomainEventHandler(
    IServiceScopeFactory serviceScopeFactory
    ) : IDomainEventHandler<EmailTokenCreatedDomainEvent>
{
    public async Task Handle(EmailTokenCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        using var scope = serviceScopeFactory.CreateScope();
        IEmailService emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
        ITokenRepository tokenRepository = scope.ServiceProvider.GetRequiredService<ITokenRepository>();
        IUserRepository userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
        Token? token =
            await tokenRepository
            .GetByTokenTypeAndValue(
                notification.Token,
                TokenType.Email
            ) ?? throw new SkillHiveException("Email verification token is null");
        User? user = await userRepository.GetByIdAsync(token.UserId) ?? throw new SkillHiveException("User not found");
        await emailService.SendVerificationToken(user.FirstName, user.Email!, token.Value!);
    }
}
