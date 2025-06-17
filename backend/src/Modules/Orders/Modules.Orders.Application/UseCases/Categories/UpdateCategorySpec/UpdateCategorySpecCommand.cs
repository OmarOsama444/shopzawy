using Common.Application.Messaging;

namespace Modules.Orders.Application.UseCases.Categories.UpdateCategorySpec;

public record UpdateCategorySpecCommand(
    Guid CategoryId,
    ICollection<Guid> Add,
    ICollection<Guid> Remove) : ICommand;
