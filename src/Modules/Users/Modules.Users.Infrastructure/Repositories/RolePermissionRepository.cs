using Microsoft.EntityFrameworkCore;
using Modules.Users.Domain.Entities;
using Modules.Users.Domain.Repositories;

namespace Modules.Users.Infrastructure.Repositories;

public class RolePermissionRepository(UsersDbContext usersDbContext)
    : Repository<RolePermission, UsersDbContext>(usersDbContext), IRolePermissionRepository
{
    public async Task<RolePermission?> GetByRoleAndPermissionId(Guid RoleId, Guid PermissionId)
    {
        return await context.RolePermissions.FirstOrDefaultAsync(x => x.RoleId == RoleId && x.PermissionId == PermissionId);
    }
}
