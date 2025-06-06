using FluentValidation;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Common.Domain.Exceptions;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Repositories;

namespace Modules.Orders.Application.UseCases.Spec.CreateSpecOption;

public sealed class CreateCategorySpecOptionCommandHandler(
    ISpecRepository SpecRepository,
    ISpecOptionRepository SpecOptionRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateSpecOptionsCommand, ICollection<Guid>>
{
    public async Task<Result<ICollection<Guid>>> Handle(CreateSpecOptionsCommand request, CancellationToken cancellationToken)
    {
        Specification? Specification = await SpecRepository.GetByIdAsync(request.SpecId);
        ICollection<Guid> specOptionIds = [];
        if (Specification is null)
            return new NotFoundException("Spec.Notfound", $"Category spec with id {request.SpecId} not found");
        foreach (var value in request.Values)
        {
            var specOption = await SpecOptionRepository.GetBySpecIdAndValue(request.SpecId, value);
            if (specOption is not null)
                continue;
            if (Specification.DataType == "color")
                return new NotAuthorizedException("Spec.NotAuthorized", $"Category spec color options are handled by the system");

            if (Specification.DataType == "number" && !float.TryParse(value, out var xx))
            {
                return new BadRequestException(new Dictionary<string, string[]>
                {
                    { "Invalid.data.type", new[] { $"couldn't parse {value} to a number" } }
                });
            }

            if (Specification.DataType == "booleon" && !bool.TryParse(value, out var xxx))
            {
                return new BadRequestException(new Dictionary<string, string[]>
                {
                    { "Invalid.data.type", new[] { $"couldn't parse {value} to a booleon" } }
                });
            }

            var catspecoption = SpecificationOption.Create(Specification.DataType, value, Specification.Id);
            SpecOptionRepository.Add(catspecoption);
            specOptionIds.Add(catspecoption.Id);
        }
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<ICollection<Guid>>.Success(specOptionIds);
    }
}
