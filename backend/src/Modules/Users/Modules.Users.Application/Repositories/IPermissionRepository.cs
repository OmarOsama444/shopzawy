using Common.Domain;
using Modules.Users.Domain.Entities;

namespace Modules.Users.Application.Repositories;

public interface IPermissionRepository : IRepository<Permission>
{
    public Task<Permission?> GetByName(string name);
    public Task<ICollection<Permission>> GetByRoleId(string Id);
    public Task<ICollection<Permission>> Paginate(int PageSize, int PageNumber, string? Name);
    public Task<int> Count(string? Name);
}
