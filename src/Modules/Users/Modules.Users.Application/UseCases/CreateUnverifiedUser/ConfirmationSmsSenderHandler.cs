using Modules.Common.Application.Messaging;
using Modules.Common.Domain.Exceptions;
using Modules.Users.Domain.DomainEvents;
using Modules.Users.Domain.Exceptions;
using Modules.Users.Domain.Repositories;

namespace Modules.Users.Application.UseCases.CreateUnverifiedUser;

public class ConfirmationSmsSenderHandler(
    IUnverifiedUserRepository userRepository
)
    : IDomainEventHandler<UnverifiedUserCreatedDomainEvent>
{
    public async Task Handle(UnverifiedUserCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(notification.UserId);
        if (user is null)
            throw new SkillHiveException($"User With id {notification.UserId} not found module {nameof(ConfirmationSmsSenderHandler)}");
        if (string.IsNullOrEmpty(user.PhoneNumber))
            return;
        // TODO ADD SMS SENDER HERE
    }

}
