namespace Modules.Users.Infrastructure.OutBox;

public class OutBoxOptions
{
    public int BatchSize { get; set; }
    public int TimeSpanInSeconds { get; set; }
}
