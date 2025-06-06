using Modules.Common.Application.Messaging;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Application.UseCases.Categories.CreateCategory;

public record CreateCategoryCommand(
    int Order,
    Guid? Parent_category_id,
    ICollection<Guid> Spec_ids,
    IDictionary<Language, string> Names,
    IDictionary<Language, string> Descriptions,
    IDictionary<Language, string> Image_urls
) : ICommand<Guid>;
