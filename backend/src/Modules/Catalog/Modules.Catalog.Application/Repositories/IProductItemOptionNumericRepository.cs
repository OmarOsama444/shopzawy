using Common.Application;
using Modules.Catalog.Domain.Entities;

namespace Modules.Catalog.Application.Repositories;

public interface IProductItemOptionNumericRepository : IRepository<ProductItemOptionNumeric>
{
    public Task<ProductItemOptionNumeric?> GetByIdAndValueAndSpecId(Guid Id, float Value, Guid SpecId);
}
