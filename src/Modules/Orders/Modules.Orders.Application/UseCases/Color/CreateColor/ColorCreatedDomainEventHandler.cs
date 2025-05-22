using Modules.Common.Application.Messaging;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Domain.DomainEvents;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Repositories;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Application.UseCases.CreateColor;

public class ColorCreatedDomainEventHandler(
    ISpecRepository specRepository,
    ISpecOptionRepository specOptionRepository,
    IUnitOfWork unitOfWork) : IDomainEventHandler<ColorCreatedDomainEvent>
{
    public async Task Handle(ColorCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var specs = await specRepository.GetByDataType(SpecDataType.Color);
        foreach (var spec in specs)
        {
            var specOption = SpecificationOption.Create(spec.DataType, notification.code, spec.Id);
            specOptionRepository.Add(specOption);
        }
        await unitOfWork.SaveChangesAsync();
    }
}
