using Common.Domain;
using Modules.Catalog.Domain.Entities;

namespace Modules.Catalog.Application.Repositories;

public interface ICategorySpecRepositroy : IRepository<CategorySpec>
{
    public Task<CategorySpec?> GetByCategoryIdAndSpecId(int categoryId, Guid specId);
}
