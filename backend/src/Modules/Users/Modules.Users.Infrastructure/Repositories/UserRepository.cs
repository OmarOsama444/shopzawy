using Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Modules.Users.Application.Repositories;
using Modules.Users.Domain.Entities;

namespace Modules.Users.Infrastructure.Repositories;

public class UserRepository(UsersDbContext usersDbContext) : Repository<User, UsersDbContext>(usersDbContext), IUserRepository
{
    public async Task<User?> GetByConfirmedEmail(string Email)
    {
        return await context.users
                .FirstOrDefaultAsync(x => x.Email == Email && x.EmailConfirmed == true);
    }

}
