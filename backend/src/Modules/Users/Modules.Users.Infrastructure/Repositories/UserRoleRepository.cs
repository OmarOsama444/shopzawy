using Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Modules.Users.Application.Repositories;
using Modules.Users.Domain.Entities;

namespace Modules.Users.Infrastructure.Repositories;

public class UserRoleRepository(UsersDbContext usersDbContext)
    : Repository<UserRole, UsersDbContext>(usersDbContext), IUserRoleRepository
{
    public async Task<UserRole?> GetByUserRoleId(Guid UserId, string RoleId)
    {
        return await context
            .userRoles
            .FirstOrDefaultAsync(
                x =>
                    x.UserId == UserId &&
                    x.RoleId == RoleId);
    }
}
