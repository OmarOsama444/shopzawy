using Common.Domain;
using Modules.Orders.Domain.Entities;

namespace Modules.Orders.Application.Repositories;

public interface IProductItemOptionsRepository : IRepository<ProductItemOptions>
{
    public Task<ProductItemOptions?> GetByIdAndValueAndSpecId(Guid Id, string Value, Guid SpecId);
}
