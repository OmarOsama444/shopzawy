using Modules.Users.Application;

namespace Modules.Users.Infrastructure.Data;

public class UnitOfWork(UsersDbContext ordersDbContext) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(CancellationToken token = default)
    {
        return await ordersDbContext.SaveChangesAsync(token);
    }

}
