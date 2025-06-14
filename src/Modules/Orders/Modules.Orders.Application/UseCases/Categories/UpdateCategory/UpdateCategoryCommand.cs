using Modules.Common.Application.Messaging;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Application.UseCases.UpdateCategory;

public record UpdateCategoryCommand(
    Guid id,
    int? order,
    IDictionary<Language, string> names,
    IDictionary<Language, string> descriptions,
    IDictionary<Language, string> imageUrls) : ICommand;
