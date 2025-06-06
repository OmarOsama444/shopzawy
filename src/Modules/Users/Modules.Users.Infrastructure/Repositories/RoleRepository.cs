using Modules.Users.Domain.Entities;
using Modules.Users.Domain.Repositories;

namespace Modules.Users.Infrastructure.Repositories;

public class RoleRepository(UsersDbContext usersDbContext)
    : Repository<Role, UsersDbContext>(usersDbContext), IRoleRepository
{ }
