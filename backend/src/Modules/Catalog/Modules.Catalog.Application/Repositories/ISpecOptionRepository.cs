using Common.Application;
using Modules.Catalog.Domain.Entities;

namespace Modules.Catalog.Application.Repositories;

public interface ISpecOptionRepository : IRepository<SpecificationOption>
{
    Task<ICollection<SpecificationOption>> GetBySpecId(Guid id);
    Task<SpecificationOption?> GetBySpecIdAndValue(Guid id, string value);
}
