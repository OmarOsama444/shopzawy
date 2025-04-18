// using Modules.Common.Application.EventBus;
// using Modules.Common.Application.Messaging;
// using Modules.Common.Domain.Exceptions;
// using Modules.Users.Domain;
// using Modules.Users.Domain.Exceptions;

// namespace Modules.Users.Application.DomainEventHandlers;

// public class UserVerifiedDomainEventHandler(
//     IEventBus eventBus,
//     IUserRepository userRepository) : IDomainEventHandler<UserVerifiedDomainEvent>
// {
//     public async Task Handle(UserVerifiedDomainEvent notification, CancellationToken cancellationToken)
//     {
//         User? user = await userRepository.GetByIdAsync(notification.UserId);
//         if (user is null)
//             throw new SkillHiveException("User not found in domain event handler");
//         UserCreatedIntegrationEvent integrationEvent = UserCreatedIntegrationEvent.Create(
//             notification.Id,
//             notification.CreatedOnUtc,
//             user.Id,
//             user.FirstName,
//             user.LastName,
//             user.Email,
//             user.PhoneNumber,
//             user.Role
//         );
//         await eventBus.PublishAsync(integrationEvent, cancellationToken);
//     }

// }
