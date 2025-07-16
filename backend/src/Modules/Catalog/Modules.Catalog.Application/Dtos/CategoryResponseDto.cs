namespace Modules.Catalog.Application.Dtos;

public class CategoryResponeDto
{
    public IDictionary<int, string> CategoryPath { get; set; } = new Dictionary<int, string>();
    public ICollection<TranslatedSpecStatisticsDto> SpecificationsCount { get; set; } = [];
    public ICollection<TranslatedSpecResponseDto> Specifications { get; set; } = [];
    public TranslatedCategoryResponseDto Current { get; set; } = default!;
    public TranslatedCategoryResponseDto? Parent { get; init; }
    public ICollection<TranslatedCategoryResponseDto> Children { get; set; } = [];
}