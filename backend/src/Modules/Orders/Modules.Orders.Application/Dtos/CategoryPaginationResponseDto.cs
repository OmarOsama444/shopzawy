namespace Modules.Orders.Application.Dtos;

public class CategoryPaginationResponseDto
{
    public Guid Id { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public int Order { get; set; }
    public Guid parentCategoryId { get; set; }
    public int NumberOfProducts { get; set; }
    public int NumberOfChildren { get; set; }
    public CategoryPaginationResponseDto()
    {

    }
    public CategoryPaginationResponseDto(
        Guid id,
        string categoryName,
        int order,
        int numberOfProducts,
        int numberOfChildren)
    {
        Id = id;
        CategoryName = categoryName;
        Order = order;
        NumberOfProducts = numberOfProducts;
        NumberOfChildren = numberOfChildren;
    }
}
