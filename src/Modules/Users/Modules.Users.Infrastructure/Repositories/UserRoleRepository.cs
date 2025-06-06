using Modules.Users.Domain.Entities;
using Modules.Users.Domain.Repositories;

namespace Modules.Users.Infrastructure.Repositories;

public class UserRoleRepository(UsersDbContext usersDbContext)
    : Repository<UserRole, UsersDbContext>(usersDbContext), IUserRoleRepository
{ }
