
using FluentValidation;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Repositories;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Application.UseCases.Spec.CreateSpec;

public sealed class CreateSpecCommandHandler(
    ISpecRepository specRepository,
    ISpecTranslationRepository specTranslationRepository,
    IUnitOfWork unitOfWork
) : ICommandHandler<CreateSpecCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateSpecCommand request, CancellationToken cancellationToken)
    {
        var spec = Specification.Create(request.DataType);
        specRepository.Add(spec);
        foreach (var specName in request.SpecNames)
        {
            specTranslationRepository.Add(
                SpecificationTranslation.Create(spec.Id, specName.Key, specName.Value)
            );
        }
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return spec.Id;
    }
}
