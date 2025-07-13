using Modules.Catalog.Domain.ValueObjects;

namespace Modules.Catalog.Application.Dtos;

public class TranslatedSpecStatisticsDto
{
    public Guid Id { get; set; }
    public SpecDataType DataType { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public int TotalProducts { get; set; }
    public DateTime CreatedOnUtc { get; set; }
}