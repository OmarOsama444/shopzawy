using Modules.Users.Application.Abstractions;
using Modules.Users.Domain.Entities;

namespace Modules.Users.Domain.Repositories;

public interface IPermissionRepository : IRepository<Permission>
{
    public Task<Permission?> GetByName(string name);
    public Task<ICollection<Permission>> GetByRoleId(Guid Id);
    public Task<ICollection<Permission>> Paginate(int PageSize, int PageNumber, string? Name);
    public Task<int> Count(string? Name);
}
