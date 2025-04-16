using Modules.Orders.Application.Abstractions;

namespace Modules.Orders.Infrastructure.Data;

public class UnitOfWork(OrdersDbContext ordersDbContext) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(CancellationToken token = default)
    {
        return await ordersDbContext.SaveChangesAsync(token);
    }

    public async Task BeginTransactionAsync()
    {
        await ordersDbContext.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        await ordersDbContext.Database.CommitTransactionAsync();
    }

    public async Task RollBackTransactionAsync()
    {
        await ordersDbContext.Database.RollbackTransactionAsync();
    }
}
