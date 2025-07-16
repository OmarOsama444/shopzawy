using System.Data;
using Microsoft.EntityFrameworkCore.Storage;
using Modules.Catalog.Application.Abstractions;

namespace Modules.Catalog.Infrastructure.Data;

public class UnitOfWork(OrdersDbContext ordersDbContext) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(CancellationToken token = default)
    {
        return await ordersDbContext.SaveChangesAsync(token);
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await ordersDbContext.Database.BeginTransactionAsync();
    }
}
