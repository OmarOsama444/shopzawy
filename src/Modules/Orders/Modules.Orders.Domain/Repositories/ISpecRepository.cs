using Modules.Orders.Domain.Entities;
using Modules.Users.Application.Abstractions;

namespace Modules.Orders.Domain.Repositories;

public interface ISpecRepository : IRepository<Specification>
{
    public Task<Specification?> GetByNameAndCategoryName(string name, string categoryName);
}
