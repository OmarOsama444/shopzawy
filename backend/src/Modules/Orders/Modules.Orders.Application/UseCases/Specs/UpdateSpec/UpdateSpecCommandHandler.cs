using Common.Application.Messaging;
using Common.Domain;
using Common.Domain.Exceptions;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Application.Repositories;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Exceptions;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Application.UseCases.Specs.UpdateSpec;

public sealed class UpdateSpecCommandHandler(
    ISpecRepository SpecRepository,
    ISpecOptionRepository SpecOptionRepository,
    ISpecTranslationRepository specTranslationRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateSpecCommand>
{
    public async Task<Result> Handle(UpdateSpecCommand request, CancellationToken cancellationToken)
    {
        Specification? Specification = await SpecRepository.GetByIdAsync(request.Id);
        if (Specification is null)
            return new SpecificationNotFoundException(request.Id);
        if (Specification.DataType != SpecDataType.String)
            return new NotAuthorizedException("Spec.NotAuthorized", $"You can only add values for string type specs");

        foreach (var optionValue in request.Add)
        {
            var specOption = await SpecOptionRepository.GetBySpecIdAndValue(request.Id, optionValue);
            if (specOption is null)
            {
                specOption = SpecificationOption.Create(optionValue, Specification.Id);
                SpecOptionRepository.Add(specOption);
            }
        }

        foreach (var optionValue in request.Remove)
        {
            var specOption = await SpecOptionRepository.GetBySpecIdAndValue(request.Id, optionValue);
            if (specOption is not null)
                SpecOptionRepository.Remove(specOption);
        }

        foreach (var specName in request.SpecNames)
        {
            var specTranslation = await specTranslationRepository.GetBySpecIdAndLanguage(request.Id, specName.Key);
            if (specTranslation is null)
            {
                specTranslationRepository.Add(
                    SpecificationTranslation.Create(request.Id, specName.Key, specName.Value)
                );
            }
            else
            {
                specTranslation.Name = specName.Value;
                specTranslationRepository.Update(specTranslation);
            }
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
