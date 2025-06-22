using Common.Application.Messaging;
using Common.Domain.ValueObjects;

namespace Modules.Orders.Application.UseCases.Specs.UpdateSpec;

public record UpdateSpecCommand(Guid Id, IDictionary<Language, string> SpecNames, ICollection<string> Add, ICollection<string> Remove) : ICommand;
