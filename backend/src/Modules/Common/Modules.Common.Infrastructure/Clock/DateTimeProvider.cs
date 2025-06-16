using Modules.Common.Application.Clock;

namespace Modules.Common.Infrastructure.Clock;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now => DateTime.Now;

    public DateTime UtcNow => DateTime.UtcNow;

}