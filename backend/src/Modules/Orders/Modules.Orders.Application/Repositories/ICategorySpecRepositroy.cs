using Common.Domain;
using Modules.Orders.Domain.Entities;

namespace Modules.Orders.Domain.Repositories;

public interface ICategorySpecRepositroy : IRepository<CategorySpec>
{
    public Task<CategorySpec?> GetByCategoryIdAndSpecId(Guid categoryId, Guid specId);
}
