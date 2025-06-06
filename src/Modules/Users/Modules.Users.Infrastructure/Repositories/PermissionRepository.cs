using Modules.Users.Domain.Entities;
using Modules.Users.Domain.Repositories;

namespace Modules.Users.Infrastructure.Repositories;

public class PermissionRepository(UsersDbContext usersDbContext)
    : Repository<Permission, UsersDbContext>(usersDbContext), IPermissionRepository
{ }
