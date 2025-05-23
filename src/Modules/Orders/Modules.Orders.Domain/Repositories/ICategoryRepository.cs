using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.ValueObjects;
using Modules.Users.Application.Abstractions;

namespace Modules.Orders.Domain.Repositories;

public interface ICategoryRepository : IRepository<Category>
{
    public Task<ICollection<MainCategoryResponse>> Children(Guid Id, Language lang_code);
    public Task<IDictionary<Guid, string>> GetCategoryPath(Guid Id, Language LangCode);
    public Task<ICollection<MainCategoryResponse>> GetMainCategories(Language lang_code);
    public Task<ICollection<CategoryPaginationResponse>> Paginate(int pageNumber, int pageSize, string? nameFilter, Language langCode);
    public Task<int> TotalCategories(string? nameFilter, Language LangCode);
    public void AddTranslation(CategoryTranslation categoryTranslation);
}

public class MainCategoryResponse
{
    public Guid Id { get; set; }
    public Guid parentCategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int order { get; set; }
    public string? ImageUrl { get; set; }
}

public class CategoryPaginationResponse
{
    public Guid Id { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public int Order { get; set; }
    public Guid parentCategoryId { get; set; }
    public int NumberOfProducts { get; set; }
    public int NumberOfChildren { get; set; }
    public CategoryPaginationResponse()
    {

    }
    public CategoryPaginationResponse(
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
