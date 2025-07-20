using Common.Application;
using Common.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Common.Infrastructure;

public abstract class Repository<TEntity, TDbContext> : IRepository<TEntity>
    where TEntity : Entity
    where TDbContext : DbContext
{
    public TDbContext context { get; set; }
    public Repository(TDbContext dbContext)
    {
        context = dbContext;
    }
    public void Add(TEntity entity)
    {
        context.Set<TEntity>().Add(entity);
    }
    public void Remove(TEntity entity)
    {
        context.Set<TEntity>().Remove(entity);
    }

    public void Update(TEntity entity)
    {
        context.Set<TEntity>().Update(entity);
    }
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await context.Set<TEntity>().ToListAsync();
    }

    public virtual async Task<TEntity?> GetByIdAsync(params object[] id)
    {
        return await context.Set<TEntity>().FindAsync(id);
    }
}
