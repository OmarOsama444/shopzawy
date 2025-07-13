namespace Modules.Catalog.Domain.Elastic;

public class LocalizedField
{
    public string? En { get; set; } = string.Empty;
    public string? Ar { get; set; } = string.Empty;
    public static LocalizedField Create(string? En, string? Ar)
    {
        return new LocalizedField() { En = En, Ar = Ar };
    }
}
