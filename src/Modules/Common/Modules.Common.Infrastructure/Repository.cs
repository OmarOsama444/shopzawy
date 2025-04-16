using Microsoft.EntityFrameworkCore;
using Modules.Common.Domain.Entities;
using Modules.Users.Application.Abstractions;

namespace Modules.Users.Infrastructure.Repositories;

public abstract class Repository<TEntity, TDbContext>(TDbContext context) : IRepository<TEntity>
    where TEntity : Entity
    where TDbContext : DbContext
{
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

    public virtual async Task<TEntity?> GetByIdAsync(object id)
    {
        return await context.Set<TEntity>().FindAsync(id);
    }
}
