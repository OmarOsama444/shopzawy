using FluentValidation;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Common.Domain.Exceptions;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Repositories;

namespace Modules.Orders.Application.UseCases.CreateSpecOption;

public record CreateSpecOptionCommand(Guid specId, string value) : ICommand<Guid>;

public sealed class CreateCategorySpecOptionCommandHandler(
    ISpecRepository SpecRepository,
    ISpecOptionRepository SpecOptionRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateSpecOptionCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateSpecOptionCommand request, CancellationToken cancellationToken)
    {
        Specification? Specification = await SpecRepository.GetByIdAsync(request.specId);
        if (Specification is null)
            return new NotFoundException("Spec.Notfound", $"Category spec with id {request.specId} not found");

        if (Specification.DataType == "color")
            return new NotAuthorizedException("Spec.NotAuthorized", $"Category spec color options are handled by the system");

        if (Specification.DataType == "number" && !float.TryParse(request.value, out var xx))
            return new BadRequestException(new Dictionary<string, string[]>
            {
                { "Invalid.data.type", new[] { $"couldn't parse {request.value} to a number" } }
            });

        if (Specification.DataType == "booleon" && !bool.TryParse(request.value, out var xxx))
            return new BadRequestException(new Dictionary<string, string[]>
            {
                { "Invalid.data.type", new[] { $"couldn't parse {request.value} to a booleon" } }
            });

        var catspecoption = SpecificationOption.Create(request.value, Specification.Id);
        SpecOptionRepository.Add(catspecoption);
        await unitOfWork.SaveChangesAsync();
        return catspecoption.Id;
    }
}

internal class CreateCategorySpecOptionCommandValidator : AbstractValidator<CreateSpecOptionCommand>
{
    public CreateCategorySpecOptionCommandValidator()
    {
        RuleFor(x => x.specId).NotEmpty();
        RuleFor(x => x.value).NotEmpty();
    }
}