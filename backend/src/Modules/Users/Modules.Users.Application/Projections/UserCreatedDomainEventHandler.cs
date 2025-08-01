using Common.Application.EventBus;
using Common.Application.Messaging;
using Common.Domain.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using Modules.Users.Application.Repositories;
using Modules.Users.Domain;
using Modules.Users.Domain.Entities;
using Modules.Users.IntegrationEvents;

namespace Modules.Users.Application.Projections
{
    public class UserCreatedDomainEventHandler(IEventBus eventBus, IServiceScopeFactory serviceScopeFactory) : IDomainEventHandler<UserCreatedDomainEvent>
    {
        public async Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            using var scope = serviceScopeFactory.CreateScope();
            IUserRepository userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
            var user = await userRepository.GetByIdAsync(notification.UserId) ?? throw new SkillHiveException($"User with id {notification.UserId} not found");
            UserCreatedIntegrationEvent userCreatedIntegrationEvent = UserCreatedIntegrationEvent.Create(
                notification.UserId,
                user.FirstName,
                user.LastName,
                user.Email
            );
            await eventBus.PublishAsync<UserCreatedIntegrationEvent>(userCreatedIntegrationEvent, cancellationToken);
        }

    }
}

