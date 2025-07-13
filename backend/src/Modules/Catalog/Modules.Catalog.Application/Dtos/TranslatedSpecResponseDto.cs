using Modules.Catalog.Domain.ValueObjects;

namespace Modules.Catalog.Application.Dtos;

public class TranslatedSpecResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public SpecDataType DataType { get; set; }
    public string[] Options { get; set; } = [];
}
