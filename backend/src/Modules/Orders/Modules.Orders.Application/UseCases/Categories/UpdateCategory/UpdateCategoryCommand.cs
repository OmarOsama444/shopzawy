using Common.Application.Messaging;
using Common.Domain.ValueObjects;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Application.UseCases.UpdateCategory;

public record UpdateCategoryCommand(
    Guid Id,
    int? Order,
    ICollection<Guid> Add,
    ICollection<Guid> Remove,
    IDictionary<Language, string> Names,
    IDictionary<Language, string> Descriptions,
    IDictionary<Language, string> ImageUrls) : ICommand;
