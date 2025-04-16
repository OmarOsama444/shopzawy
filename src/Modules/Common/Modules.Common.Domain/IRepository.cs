using Modules.Common.Domain.Entities;

namespace Modules.Users.Application.Abstractions;

public interface IRepository<TEntity> where TEntity : Entity
{
    void Add(TEntity entity);
    void Remove(TEntity entity);
    void Update(TEntity entity);
    Task<TEntity?> GetByIdAsync(object id);
    Task<IEnumerable<TEntity>> GetAllAsync();
}

