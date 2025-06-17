using Common.Application.Clock;

namespace Common.Infrastructure.Clock;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now => DateTime.Now;

    public DateTime UtcNow => DateTime.UtcNow;

}