using Common.Domain;
using Common.Domain.ValueObjects;
using Modules.Orders.Application.Dtos;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Entities.Views;

namespace Modules.Orders.Application.Repositories;

public interface ISpecStatisticRepository : IRepository<SpecificationStatistics>
{
    public Task<SpecificationStatistics?> GetByIdAndValueAsync(Guid id, string value, CancellationToken cancellationToken = default);
    public Task<ICollection<TranslatedSpecStatisticsDto>> GetByCategoryId(Guid Id, Guid[] path, Language langCode);
}
