using Common.Application.Messaging;
using Common.Domain.ValueObjects;

namespace Modules.Catalog.Application.UseCases.Categories.UpdateCategory;

public record UpdateCategoryCommand(
    Guid Id,
    int? Order,
    ICollection<Guid> Add,
    ICollection<Guid> Remove,
    IDictionary<Language, string> Names,
    IDictionary<Language, string> Descriptions,
    IDictionary<Language, string> ImageUrls,
    IDictionary<Language, string> AddNames,
    IDictionary<Language, string> AddDescriptions,
    IDictionary<Language, string> AddImageUrls,
    ICollection<Language> RemoveTranslation) : ICommand;
