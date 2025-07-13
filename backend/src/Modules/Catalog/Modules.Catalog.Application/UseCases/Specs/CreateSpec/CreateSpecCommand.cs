
using Common.Application.Messaging;
using Common.Domain.ValueObjects;
using Modules.Catalog.Domain.ValueObjects;

namespace Modules.Catalog.Application.UseCases.Specs.CreateSpec;

public record CreateSpecCommand(IDictionary<Language, string> SpecNames, SpecDataType DataType) : ICommand<Guid>;
