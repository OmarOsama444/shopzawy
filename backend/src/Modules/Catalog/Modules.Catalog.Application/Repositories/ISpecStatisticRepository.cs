using Common.Domain;
using Common.Domain.ValueObjects;
using Modules.Catalog.Application.Dtos;
using Modules.Catalog.Domain.Entities.Views;
namespace Modules.Catalog.Application.Repositories;

public interface ISpecStatisticRepository : IRepository<SpecificationStatistics>
{
    public Task<SpecificationStatistics?> GetByIdAndValueAsync(Guid id, string value, CancellationToken cancellationToken = default);
    public Task<ICollection<TranslatedSpecStatisticsDto>> GetByCategoryId(Guid Id, Guid[] path, Language langCode);
}
