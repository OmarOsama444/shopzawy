using Common.Application.Messaging;
using Common.Domain;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Application.Repositories;
using Modules.Orders.Domain.Entities;

namespace Modules.Orders.Application.UseCases.Specs.CreateSpec;

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
