using Common.Application.Messaging;
using Common.Domain.ValueObjects;

namespace Modules.Catalog.Application.UseCases.Categories.CreateCategory;

public record CreateCategoryCommand(
    int Order,
    Guid ParentCategoryId,
    ICollection<Guid> SpecIds,
    IDictionary<Language, string> Names,
    IDictionary<Language, string> Descriptions,
    IDictionary<Language, string> ImageUrls
) : ICommand<Guid>;
