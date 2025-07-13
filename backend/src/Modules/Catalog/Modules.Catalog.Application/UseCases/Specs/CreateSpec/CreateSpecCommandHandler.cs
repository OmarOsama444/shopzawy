using Common.Application.Messaging;
using Common.Domain;
using Modules.Catalog.Application.Abstractions;
using Modules.Catalog.Application.Repositories;
using Modules.Catalog.Domain.Entities;
using Modules.Catalog.Domain.Entities.Translation;

namespace Modules.Catalog.Application.UseCases.Specs.CreateSpec;

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
