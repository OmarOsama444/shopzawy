using FluentValidation;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Common.Domain.Exceptions;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Repositories;

namespace Modules.Orders.Application.UseCases.CreateCategorySpecOption;

public record CreateCategorySpecOptionCommand(string categoryName, string specName, string value) : ICommand<Guid>;

public sealed class CreateCategorySpecOptionCommandHandler(
    ISpecRepository SpecRepository,
    ISpecOptionRepository SpecOptionRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateCategorySpecOptionCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateCategorySpecOptionCommand request, CancellationToken cancellationToken)
    {
        Specification? categorySpecification = await SpecRepository.GetByNameAndCategoryName(request.specName, request.categoryName);
        if (categorySpecification is null)
            return new NotFoundException("Category.Spec.Notfound", $"Category spec with name {request.specName} not found");

        if (categorySpecification.DataType == "color")
            return new NotAuthorizedException("Category.Spec.NotAuthorized", $"Category spec color options are handled by the system");

        if (categorySpecification.DataType == "number" && !float.TryParse(request.value, out var xx))
            return new BadRequestException(new Dictionary<string, string[]>
            {
                { "Invalid.data.type", new[] { $"couldn't parse {request.value} to a number" } }
            });
        if (categorySpecification.DataType == "booleon" && !bool.TryParse(request.value, out var xxx))
            return new BadRequestException(new Dictionary<string, string[]>
            {
                { "Invalid.data.type", new[] { $"couldn't parse {request.value} to a booleon" } }
            });
        var catspecoption = SpecificationOption.Create(request.value, categorySpecification.Id);
        SpecOptionRepository.Add(catspecoption);
        await unitOfWork.SaveChangesAsync();
        return catspecoption.Id;
    }
}

internal class CreateCategorySpecOptionCommandValidator : AbstractValidator<CreateCategorySpecOptionCommand>
{
    public CreateCategorySpecOptionCommandValidator()
    {
        RuleFor(x => x.categoryName).NotEmpty();
        RuleFor(x => x.specName).NotEmpty();
        RuleFor(x => x.value).NotEmpty();
    }
}