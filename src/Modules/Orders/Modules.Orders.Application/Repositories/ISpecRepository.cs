using Modules.Orders.Application.Dtos;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.ValueObjects;
using Modules.Users.Application.Abstractions;

namespace Modules.Orders.Application.Repositories;

public interface ISpecRepository : IRepository<Specification>
{
    public Task<ICollection<Specification>> GetByDataType(SpecDataType dataType);
    public Task<ICollection<TranslatedSpecResponseDto>> Paginate(int pageNumber, int pageSize, string? name, Language langCode);
    public Task<int> Total(string? name, Language langCode);
    public Task<ICollection<TranslatedSpecResponseDto>> GetByCategoryId(Language langCode, params Guid[] categoryId);
}
