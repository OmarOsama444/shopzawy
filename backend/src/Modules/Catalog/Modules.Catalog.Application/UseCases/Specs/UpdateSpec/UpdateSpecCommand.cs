using Common.Application.Messaging;
using Common.Domain.ValueObjects;

namespace Modules.Catalog.Application.UseCases.Specs.UpdateSpec;

public record UpdateSpecCommand(Guid Id, IDictionary<Language, string> SpecNames, ICollection<string> Add, ICollection<string> Remove) : ICommand;
