namespace Common.Application.EventBus
{
    public interface IEventBus
    {
        Task PublishAsync<T>(T IntegrationEvent, CancellationToken token = default)
            where T : IIntegrationEvent;
    }
}
