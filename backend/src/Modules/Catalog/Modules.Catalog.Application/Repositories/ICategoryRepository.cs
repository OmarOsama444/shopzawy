using Common.Domain;
using Common.Domain.ValueObjects;
using Modules.Catalog.Application.Dtos;
using Modules.Catalog.Domain.Entities;
using Modules.Catalog.Domain.Entities.Translation;

namespace Modules.Catalog.Application.Repositories;

public interface ICategoryRepository : IRepository<Category>
{
    public Task<TranslatedCategoryResponseDto?> GetById(int id, Language langCode);
    public Task<TranslatedCategoryResponseDto?> GetParentById(int id, Language langCode);
    public Task<ICollection<TranslatedCategoryResponseDto>> GetChildrenById(int Id, Language langCode);
    public Task<IDictionary<int, string>> GetCategoryPath(int Id, Language langCode);
    public Task<ICollection<CategoryPaginationResponseDto>> Paginate(int pageNumber, int pageSize, string? nameFilter, Language langCode);
    public Task<int> TotalCategories(string? nameFilter, Language LangCode);
}
