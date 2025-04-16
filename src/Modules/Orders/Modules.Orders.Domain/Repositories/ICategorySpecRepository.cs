using Modules.Orders.Domain.Entities;
using Modules.Users.Application.Abstractions;

namespace Modules.Orders.Domain.Repositories;

public interface ICategorySpecRepository : IRepository<CategorySpec>
{
    Task<CategorySpec?> GetByCategoryNameAndSpecId(string categoryName, Guid specId);
}
