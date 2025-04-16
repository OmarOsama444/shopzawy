namespace Modules.Common.Application.Clock;

public interface IDateTimeProvider
{
    public DateTime Now { get; }
    public DateTime UtcNow { get; }
}
