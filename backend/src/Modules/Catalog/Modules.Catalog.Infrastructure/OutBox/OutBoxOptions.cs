namespace Modules.Catalog.Infrastructure.OutBox;

public class OutBoxOptions
{
    public int BatchSize { get; set; }
    public int TimeSpanInSeconds { get; set; }
}
