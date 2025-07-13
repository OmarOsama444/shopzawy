using Common.Domain;
using Common.Domain.ValueObjects;
using Modules.Catalog.Application.Dtos;
using Modules.Catalog.Domain.Entities;
using Modules.Catalog.Domain.Entities.Translation;

namespace Modules.Catalog.Application.Repositories;

public interface ICategoryRepository : IRepository<Category>
{
    public Task<TranslatedCategoryResponseDto?> GetById(Guid id, Language langCode);
    public Task<TranslatedCategoryResponseDto?> GetParentById(Guid id, Language langCode);
    public Task<ICollection<TranslatedCategoryResponseDto>> GetChildrenById(Guid Id, Language langCode);
    public Task<IDictionary<Guid, string>> GetCategoryPath(Guid Id, Language langCode);
    public Task<ICollection<CategoryPaginationResponseDto>> Paginate(int pageNumber, int pageSize, string? nameFilter, Language langCode);
    public Task<int> TotalCategories(string? nameFilter, Language LangCode);
    public void AddTranslation(CategoryTranslation categoryTranslation);
}
