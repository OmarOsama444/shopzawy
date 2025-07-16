
namespace Modules.Catalog.Application.Dtos;

public class TranslatedCategoryResponseDto
{
    public int Id { get; set; }
    public int? ParentCategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Order { get; set; }
    public string? ImageUrl { get; set; }
    public int[] Path { get; set; } = [];
}
