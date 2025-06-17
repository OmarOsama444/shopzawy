using Common.Application.Messaging;

namespace Modules.Orders.Application.UseCases.Spec.CreateSpecOption;

public record UpdateSpecOptionsCommand(Guid Id, ICollection<string> Add, ICollection<string> Remove) : ICommand;
