namespace Modules.Orders.Application.Abstractions;

public interface IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken token = default);
    public Task BeginTransactionAsync();
    public Task CommitTransactionAsync();
    public Task RollBackTransactionAsync();
}