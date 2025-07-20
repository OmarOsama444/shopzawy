using Common.Application;
using Modules.Catalog.Domain.Entities;

namespace Modules.Catalog.Application.Repositories;

public interface IProductItemOptionsRepository : IRepository<ProductItemOptions>
{
    public Task<ProductItemOptions?> GetByIdAndValueAndSpecId(Guid Id, string Value, Guid SpecId);
}
