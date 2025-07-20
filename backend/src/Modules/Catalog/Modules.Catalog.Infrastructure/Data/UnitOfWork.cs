using System.Data;
using Common.Application.InjectionLifeTime;
using Microsoft.EntityFrameworkCore.Storage;
using Modules.Catalog.Application.Abstractions;

namespace Modules.Catalog.Infrastructure.Data;

public class UnitOfWork(OrdersDbContext ordersDbContext) : IUnitOfWork, IScopped
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
