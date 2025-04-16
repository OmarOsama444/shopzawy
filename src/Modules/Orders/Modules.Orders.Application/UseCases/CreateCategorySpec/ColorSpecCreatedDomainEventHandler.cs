using Modules.Common.Application.Messaging;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Domain.DomainEvents;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Repositories;

namespace Modules.Orders.Application.UseCases.CreateCategorySpec;

public class ColorSpecCreatedDomainEventHandler(
    ISpecOptionRepository categorySpecificationOptionRepository,
    IColorRepository colorRepository,
    IUnitOfWork unitOfWork) :
    IDomainEventHandler<SpecCreatedDomainEvent>
{
    public async Task Handle(SpecCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var colors = await colorRepository.GetAllAsync();
        foreach (Color color in colors)
        {
            var x = SpecificationOption.Create(color.Name, notification.SpecId);
            categorySpecificationOptionRepository.Add(x);
        }
        await unitOfWork.SaveChangesAsync();
    }
}
