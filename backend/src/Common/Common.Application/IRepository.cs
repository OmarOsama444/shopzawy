using Common.Application.InjectionLifeTime;
using Common.Domain.Entities;

namespace Common.Application
{
    public interface IRepository<TEntity> : IScopped where TEntity : IEntity
    {
        void Add(TEntity entity);
        void Remove(TEntity entity);
        void Update(TEntity entity);
        Task<TEntity?> GetByIdAsync(params object[] id);
        Task<IEnumerable<TEntity>> GetAllAsync();
    }

}
