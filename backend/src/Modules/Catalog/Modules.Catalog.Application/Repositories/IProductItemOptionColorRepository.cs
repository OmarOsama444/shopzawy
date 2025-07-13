using Common.Domain;
using Modules.Catalog.Domain.Entities;

namespace Modules.Catalog.Application.Repositories;

public interface IProductItemOptionColorRepository : IRepository<ProductItemOptionColor>
{
    public Task<ProductItemOptionColor?> GetByIdAndValueAndSpecId(Guid Id, String Value, Guid SpecId);
}
