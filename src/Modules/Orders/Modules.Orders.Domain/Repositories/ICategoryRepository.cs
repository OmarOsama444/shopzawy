using Modules.Orders.Domain.Entities;
using Modules.Users.Application.Abstractions;

namespace Modules.Orders.Domain.Repositories;

public interface ICategoryRepository : IRepository<Category>
{
    public Task<ICollection<Category>> Children(string CategoryName);
    public Task<ICollection<Category>> GetMainCategories();
    public Task<ICollection<CategoryResponse>> Paginate(int pageNumber, int pageSize, string? nameFilter);
    public Task<int> TotalCategories(string? nameFilter);
}

public class CategoryResponse
{
    public string CategoryName { get; set; } = string.Empty;
    public int Order { get; set; }
    public string ParentName { get; set; } = string.Empty;
    public int NumberOfProducts { get; set; }
    public int NumberOfChildren { get; set; }
    public CategoryResponse()
    {

    }
    public CategoryResponse(
        string categoryName,
        int order,
        string parentName,
        int numberOfProducts,
        int numberOfChildren)
    {
        CategoryName = categoryName;
        Order = order;
        ParentName = parentName;
        NumberOfProducts = numberOfProducts;
        NumberOfChildren = numberOfChildren;
    }
}
