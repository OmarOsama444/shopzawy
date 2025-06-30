using Common.Domain;
using Modules.Orders.Domain.Entities;

namespace Modules.Orders.Application.Repositories;

public interface IProductItemOptionColorRepository : IRepository<ProductItemOptionColor>
{
    public Task<ProductItemOptionColor?> GetByIdAndValueAndSpecId(Guid Id, String Value, Guid SpecId);
}
