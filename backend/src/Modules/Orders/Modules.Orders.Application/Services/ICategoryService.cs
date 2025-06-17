using Common.Domain;
using Common.Domain.ValueObjects;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Application.Services;

public interface ICategoryService
{
    public Task<Result<Guid>> CreateCategory(
        int Order,
        Guid? parentCategoryId,
        ICollection<Guid> Ids,
        IDictionary<Language, string> names,
        IDictionary<Language, string> descriptions,
        IDictionary<Language, string> imageUrls
    );

}

