
using Common.Application.Messaging;
using Common.Domain.ValueObjects;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Application.UseCases.Spec.CreateSpec;

public record CreateSpecCommand(IDictionary<Language, string> SpecNames, SpecDataType DataType) : ICommand<Guid>;
