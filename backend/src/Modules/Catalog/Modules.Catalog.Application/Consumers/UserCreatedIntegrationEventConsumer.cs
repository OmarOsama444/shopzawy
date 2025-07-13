using MassTransit;
using Modules.Users.IntegrationEvents;
namespace Modules.Catalog.Application.Consumers;

public class UserCreatedIntegrationEventConsumer : IConsumer<UserCreatedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<UserCreatedIntegrationEvent> context)
    {
        var user = context.Message;
    }

}
