namespace Modules.Common.Application.EventBus;

public abstract class IntegrationEvent : IIntegrationEvent
{
    public Guid id { get; init; }
    public DateTime OccuredAt { get; init; }
    protected IntegrationEvent()
    {
        this.id = Guid.NewGuid();
        this.OccuredAt = DateTime.UtcNow;
    }
    protected IntegrationEvent(Guid id, DateTime OccuredAt)
    {
        this.id = id;
        this.OccuredAt = OccuredAt;
    }
}
