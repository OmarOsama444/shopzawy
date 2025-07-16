using Common.Domain;
using Common.Domain.ValueObjects;

namespace Modules.Catalog.Application.Services;

public interface ICategoryService
{
    public Task<Result<int>> CreateCategory(
        int Order,
        int? parentCategoryId,
        ICollection<Guid> SpecIds,
        IDictionary<Language, string> names,
        IDictionary<Language, string> descriptions,
        IDictionary<Language, string> imageUrls
    );

}

