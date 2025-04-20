using Modules.Orders.Domain.Entities;
using Modules.Users.Application.Abstractions;

namespace Modules.Orders.Domain.Repositories;

public interface ICategorySpecRepositroy : IRepository<CategorySpec>
{
    public Task UpdateCategoryName(string from, string to);
    public Task DeleteCategoryName(string from);
}
