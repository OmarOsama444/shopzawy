using Modules.Common.Application.Messaging;
using Modules.Common.Domain.Exceptions;
using Modules.Users.Domain;
using Modules.Users.Domain.DomainEvents;
using Modules.Users.Domain.Entities;
using Modules.Users.Domain.Repositories;

namespace Modules.Users.Application.UseCases.CreateUnverifiedUser;

public class ConfirmationEmailSenderHandler(
    IUnverifiedUserRepository userRepository,
    IEmailVerificationTokenRepository emailVerificationTokenRepository,
    IUnitOfWork unitOfWork
    ) : IDomainEventHandler<UnverifiedUserCreatedDomainEvent>
{
    public async Task Handle(UnverifiedUserCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(notification.UserId);
        if (user is null)
            throw new SkillHiveException($"user not found notifcationid {notification.Id} , {nameof(ConfirmationEmailSenderHandler)}");
        if (string.IsNullOrEmpty(user.Email))
            return;
        var emailToken = EmailVerificationToken.Create(notification.UserId, 60);
        emailVerificationTokenRepository.Add(emailToken);
        await unitOfWork.SaveChangesAsync();
    }

}
