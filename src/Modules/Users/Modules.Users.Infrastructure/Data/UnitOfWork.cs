using System.Data.Common;
using Microsoft.EntityFrameworkCore.Storage;
using Modules.Users.Application;

namespace Modules.Users.Infrastructure.Data;

public class UnitOfWork(UserDbContext userDbContext) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(CancellationToken token = default)
    {
        return await userDbContext.SaveChangesAsync(token);
    }

    public async Task BeginTransactionAsync()
    {
        await userDbContext.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        await userDbContext.Database.CommitTransactionAsync();
    }

    public async Task RollBackTransactionAsync()
    {
        await userDbContext.Database.RollbackTransactionAsync();
    }


}
