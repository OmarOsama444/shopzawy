using Common.Domain;
using Common.Domain.ValueObjects;
using Modules.Catalog.Application.Dtos;
using Modules.Catalog.Domain.Entities;
using Modules.Catalog.Domain.ValueObjects;

namespace Modules.Catalog.Application.Repositories;

public interface ISpecRepository : IRepository<Specification>
{
    public Task<ICollection<Specification>> GetByDataType(SpecDataType dataType);
    public Task<ICollection<TranslatedSpecResponseDto>> Paginate(int pageNumber, int pageSize, string? name, Language langCode);
    public Task<int> Total(string? name, Language langCode);
    public Task<ICollection<TranslatedSpecResponseDto>> GetByCategoryId(Guid CategoryId, Guid[] Path, Language langCode);
}
