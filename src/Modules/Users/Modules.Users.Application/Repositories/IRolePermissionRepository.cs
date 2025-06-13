using Modules.Users.Application.Abstractions;
using Modules.Users.Domain.Entities;

namespace Modules.Users.Application.Repositories;

public interface IRolePermissionRepository : IRepository<RolePermission>
{
    public Task<RolePermission?> GetByRoleAndPermissionId(string RoleId, string PermissionId);
}
