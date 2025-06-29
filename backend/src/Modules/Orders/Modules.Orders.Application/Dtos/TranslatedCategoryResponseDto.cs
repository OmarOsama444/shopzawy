using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Application.Dtos;

public class TranslatedCategoryResponseDto
{
    public Guid Id { get; set; }
    public Guid parentCategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Order { get; set; }
    public string? ImageUrl { get; set; }
    public Guid[] Path { get; set; } = [];
}
