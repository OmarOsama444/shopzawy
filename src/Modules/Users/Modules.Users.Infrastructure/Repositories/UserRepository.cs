using Microsoft.EntityFrameworkCore;
using Modules.Users.Domain.Entities;
using Modules.Users.Domain.Repositories;

namespace Modules.Users.Infrastructure.Repositories;

public class UserRepository(UsersDbContext usersDbContext) : Repository<User, UsersDbContext>(usersDbContext), IUserRepository
{
    public async Task<User?> GetByConfirmedEmail(string Email)
    {
        return await context.users
                .FirstOrDefaultAsync(x => x.Email == Email && x.EmailConfirmed == true);
    }

    public async Task<User?> GetByConfirmedPhone(string PhoneNumber)
    {
        return await context.users
                .FirstOrDefaultAsync(x => x.PhoneNumber == PhoneNumber && x.PhoneNumberConfirmed == true);
    }

}
