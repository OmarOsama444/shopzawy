using Modules.Users.Application.Abstractions;
using Modules.Users.Domain.Entities;

namespace Modules.Users.Domain.Repositories;

public interface IRoleRepository : IRepository<Role>
{
    public Task<Role?> GetByName(string Name);
    public Task<ICollection<Role>> Paginate(int PageSize, int PageNumber, string? Name);
    public Task<int> Count(string? Name);
}
