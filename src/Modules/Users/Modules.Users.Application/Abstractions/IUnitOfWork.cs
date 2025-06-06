namespace Modules.Users.Application;

public interface IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken token = default);
}