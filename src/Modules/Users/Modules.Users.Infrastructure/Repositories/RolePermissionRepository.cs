using Modules.Users.Domain.Entities;
using Modules.Users.Domain.Repositories;

namespace Modules.Users.Infrastructure.Repositories;

public class RolePermissionRepository(UsersDbContext usersDbContext)
    : Repository<RolePermission, UsersDbContext>(usersDbContext), IRolePermissionRepository
{ }
