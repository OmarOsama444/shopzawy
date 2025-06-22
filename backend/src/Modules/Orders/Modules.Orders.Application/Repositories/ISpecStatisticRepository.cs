using Common.Domain;
using Modules.Orders.Domain.Entities.Views;

namespace Modules.Orders.Application.Repositories;

public interface ISpecStatisticRepository : IRepository<SpecificationStatistics>
{
    public Task<SpecificationStatistics?> GetByIdAndValueAsync(Guid id, string value, CancellationToken cancellationToken = default);
}
