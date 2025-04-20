using Modules.Common.Domain;

namespace Modules.Orders.Application.Services;

public interface ICategoryService
{
    public Task<Result<string>> CreateCategory(
        string CategoryName,
        string Description,
        int Order,
        string ImageUrl,
        string? ParentCategoryName,
        ICollection<Guid> Ids
    );
}
