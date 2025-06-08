using Microsoft.EntityFrameworkCore;
using Modules.Users.Domain.Entities;
using Modules.Users.Domain.Repositories;

namespace Modules.Users.Infrastructure.Repositories;

public class PermissionRepository(UsersDbContext usersDbContext)
    : Repository<Permission, UsersDbContext>(usersDbContext), IPermissionRepository
{
    public async Task<int> Count(string? Name)
    {
        if (!string.IsNullOrEmpty(Name))
            return await context.permissions.Where(x => x.Name.ToLower().StartsWith(Name.ToLower())).CountAsync();
        return await context.permissions.CountAsync();
    }

    public async Task<Permission?> GetByName(string name)
    {
        return await context.permissions.FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<ICollection<Permission>> GetByRoleId(Guid Id)
    {
        return await context
            .rolePermissions
            .Include(rp => rp.Permission)
            .Where(rp => rp.RoleId == Id)
            .Select(rp => rp.Permission)
            .ToListAsync();
    }

    public async Task<ICollection<Permission>> Paginate(int PageSize, int PageNumber, string? Name)
    {
        int skip = (PageNumber - 1) * PageSize;
        var permissions = context
            .permissions
            .Skip(skip)
            .Take(PageSize);
        if (!string.IsNullOrEmpty(Name))
            permissions = permissions.Where(p => p.Name.ToLower().StartsWith(Name.ToLower()));
        return await permissions.ToListAsync();
    }
}
