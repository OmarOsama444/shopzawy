using Common.Domain;
using Common.Domain.ValueObjects;
using Modules.Orders.Application.Dtos;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Application.Repositories;

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
