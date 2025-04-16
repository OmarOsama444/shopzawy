using Modules.Orders.Domain.Entities;
using Modules.Users.Application.Abstractions;

namespace Modules.Orders.Domain.Repositories;

public interface ISpecOptionRepository : IRepository<SpecificationOption>
{
    Task<ICollection<SpecificationOption>> GetBySpecId(Guid id);
}
