using Common.Domain;
using Modules.Orders.Domain.Entities;

namespace Modules.Orders.Application.Repositories;

public interface IProductItemOptionNumericRepository : IRepository<ProductItemOptionNumeric>
{
    public Task<ProductItemOptionNumeric?> GetByIdAndValueAndSpecId(Guid Id, float Value, Guid SpecId);
}
