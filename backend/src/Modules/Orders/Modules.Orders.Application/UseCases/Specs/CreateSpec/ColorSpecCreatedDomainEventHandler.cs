using Common.Application.Messaging;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Application.Repositories;
using Modules.Orders.Domain.DomainEvents;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Application.UseCases.Specs.CreateSpec;

public class ColorSpecCreatedDomainEventHandler(
    ISpecOptionRepository specOptionRepository,
    ISpecRepository specRepository,
    IColorRepository colorRepository,
    IUnitOfWork unitOfWork) :
    IDomainEventHandler<SpecCreatedDomainEvent>
{
    public async Task Handle(SpecCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var colors = await colorRepository.GetAllAsync();
        var spec = await specRepository.GetByIdAsync(notification.SpecId);
        if (spec is not null && spec.DataType == SpecDataType.Color)
        {
            foreach (Color color in colors)
            {
                var x = SpecificationOption.Create(color.Code, notification.SpecId);
                specOptionRepository.Add(x);
            }
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
