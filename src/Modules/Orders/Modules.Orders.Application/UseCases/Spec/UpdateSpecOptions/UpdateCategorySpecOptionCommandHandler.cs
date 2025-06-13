using FluentValidation;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Common.Domain.Exceptions;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Application.Repositories;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Exceptions;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Application.UseCases.Spec.CreateSpecOption;

public sealed class UpdateCategorySpecOptionCommandHandler(
    ISpecRepository SpecRepository,
    ISpecOptionRepository SpecOptionRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateSpecOptionsCommand>
{
    public async Task<Result> Handle(UpdateSpecOptionsCommand request, CancellationToken cancellationToken)
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
                SpecOptionRepository.Add(specOption);
            }
        }

        foreach (var optionValue in request.Remove)
        {
            var specOption = await SpecOptionRepository.GetBySpecIdAndValue(request.Id, optionValue);
            if (specOption is not null)
                SpecOptionRepository.Remove(specOption);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
