using Common.Application.Messaging;
using Common.Domain;
using Common.Domain.Exceptions;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Application.Repositories;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Entities.Views;
using Modules.Orders.Domain.Exceptions;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Application.UseCases.Specs.UpdateSpec;

public sealed class UpdateSpecCommandHandler(
    ISpecRepository SpecRepository,
    ISpecOptionRepository SpecOptionRepository,
    ISpecStatisticRepository specStatisticRepository,
    ISpecTranslationRepository specTranslationRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateSpecCommand>
{
    public async Task<Result> Handle(UpdateSpecCommand request, CancellationToken cancellationToken)
    {
        Specification? Specification = await SpecRepository.GetByIdAsync(request.Id);
        if (Specification is null)
            return new SpecificationNotFoundException(request.Id);

        foreach (var optionValue in request.Add)
        {
            var specOption = await SpecOptionRepository.GetBySpecIdAndValue(request.Id, optionValue);
            if (specOption is null)
            {
                if (Specification.DataType == SpecDataType.Color)
                    return new NotAuthorizedException("Spec.NotAuthorized", $"Category spec color options are handled by the system");

                if (Specification.DataType == SpecDataType.Number && !float.TryParse(optionValue, out var xx))
                    return new BadRequestException(new Dictionary<string, string[]>
                    {
                        { "Invalid.data.type", new[] { $"couldn't parse {optionValue} to a number" } }
                    });

                specOption = SpecificationOption.Create(optionValue, Specification.Id);
                SpecificationStatistics stats = SpecificationStatistics.Create(Specification.Id, Specification.DataType, optionValue, 0);
                SpecOptionRepository.Add(specOption);
                specStatisticRepository.Add(stats);
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
            }
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
