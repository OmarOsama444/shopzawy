using FluentValidation;
using Modules.Common.Application.Validators;
using Modules.Common.Domain;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Application.Services;

public interface ICategoryService
{
    public Task<Result<Guid>> CreateCategory(
        int Order,
        Guid? parentCategoryId,
        ICollection<Guid> Ids,
        IDictionary<Language, CategoryLangData> categoryLangData
    );
}

public record CategoryLangData(string name, string description, string image_url);

