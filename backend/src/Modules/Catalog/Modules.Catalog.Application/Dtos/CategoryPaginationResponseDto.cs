namespace Modules.Catalog.Application.Dtos;

public class CategoryPaginationResponseDto
{
    public Guid Id { get; private set; }
    public int Order { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string ParentCategoryName { get; set; } = string.Empty;
    public Guid? ParentCategoryId { get; private set; }
    public int TotalChildren { get; private set; }
    public int TotalProducts { get; private set; }
    public int TotalSpecs { get; private set; }
    public DateTime CreatedOn { get; private set; }
}
