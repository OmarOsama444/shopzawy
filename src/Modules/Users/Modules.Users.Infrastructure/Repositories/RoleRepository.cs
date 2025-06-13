using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Modules.Users.Application.Repositories;
using Modules.Users.Domain.Entities;

namespace Modules.Users.Infrastructure.Repositories;

public class RoleRepository(UsersDbContext usersDbContext)
    : Repository<Role, UsersDbContext>(usersDbContext), IRoleRepository
{
    public async Task<Role?> GetByName(string name)
    {
        return await context.roles.FirstOrDefaultAsync(x => x.Name == name);
    }
    public async Task<int> Count(string? Name)
    {
        if (!string.IsNullOrEmpty(Name))
        {
            return await context
                .roles
                .Where(
                    x => x.Name.ToLower().StartsWith(Name.ToLower())
                ).CountAsync();
        }
        return await context
                .roles
                .CountAsync();
    }
    public async Task<ICollection<Role>> Paginate(int PageSize, int PageNumber, string? Name)
    {
        int skip = PageSize * (PageNumber - 1);
        if (!string.IsNullOrEmpty(Name))
        {
            return await context
                .roles
                .Skip(skip)
                .Take(PageSize)
                .Where(
                    x => x.Name.ToLower().StartsWith(Name.ToLower())
                ).ToListAsync();
        }
        return await context
                .roles
                .Skip(skip)
                .Take(PageSize)
                .ToListAsync();
    }
}
