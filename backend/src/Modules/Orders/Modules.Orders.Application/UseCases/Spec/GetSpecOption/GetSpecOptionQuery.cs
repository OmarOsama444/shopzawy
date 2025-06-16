using Modules.Common.Application.Messaging;
using Modules.Orders.Application.UseCases.Spec.Dtos;

namespace Modules.Orders.Application.UseCases.Spec.GetSpecOption;

public record GetSpecOptionQuery(Guid SpecId) : IQuery<ICollection<SpecOptionResponse>>;
