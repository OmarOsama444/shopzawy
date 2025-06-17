using Common.Domain;
using Modules.Orders.Domain.Entities;

namespace Modules.Orders.Application.Repositories;

public interface ISpecOptionRepository : IRepository<SpecificationOption>
{
    Task<ICollection<SpecificationOption>> GetBySpecId(Guid id);
    Task<SpecificationOption?> GetBySpecIdAndValue(Guid id, string value);
}
