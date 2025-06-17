using Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Modules.Users.Application.Repositories;
using Modules.Users.Domain.Entities;

namespace Modules.Users.Infrastructure.Repositories;

public class RolePermissionRepository(UsersDbContext usersDbContext)
    : Repository<RolePermission, UsersDbContext>(usersDbContext), IRolePermissionRepository
{
    public async Task<RolePermission?> GetByRoleAndPermissionId(string RoleId, string PermissionId)
    {
        return await context.RolePermissions.FirstOrDefaultAsync(x => x.RoleId == RoleId && x.PermissionId == PermissionId);
    }
}
