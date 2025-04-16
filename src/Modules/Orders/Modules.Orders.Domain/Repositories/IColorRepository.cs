using Modules.Orders.Domain.Entities;
using Modules.Users.Application.Abstractions;

namespace Modules.Orders.Domain.Repositories;

public interface IColorRepository : IRepository<Color>
{
    public Task<Color?> GetByName(string name);
}
