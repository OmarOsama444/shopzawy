using System.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace Modules.Catalog.Application.Abstractions;

public interface IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken token = default);
    public Task<IDbContextTransaction> BeginTransactionAsync();

}