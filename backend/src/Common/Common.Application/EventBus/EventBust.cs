using MassTransit;

namespace Common.Application.EventBus
{
    public class EventBus(IBus bus) : IEventBus
    {
        public async Task PublishAsync<T>(T IntegrationEvent, CancellationToken token = default) where T : IIntegrationEvent
        {
            await bus.Publish(IntegrationEvent, token);
        }
    }
}
