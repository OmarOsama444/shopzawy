namespace Modules.Orders.Application.Dtos;

public class FullTranslatedCategoryResponseDto
{
    public TranslatedCategoryResponseDto Current { get; set; } = default!;
    public TranslatedCategoryResponseDto? Parent { get; set; } = null;
    public ICollection<TranslatedCategoryResponseDto> Children { get; set; } = [];

}
