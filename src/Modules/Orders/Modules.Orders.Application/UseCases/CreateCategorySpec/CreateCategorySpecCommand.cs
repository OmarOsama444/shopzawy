
using FluentValidation;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Common.Domain.Exceptions;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Exceptions;
using Modules.Orders.Domain.Repositories;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Application.UseCases;

public record CreateCategorySpecCommand(string categoryName, string specName, string dataType) : ICommand<Guid>;

public sealed class CreateCategorySpecCommandHandler(
    ISpecRepository SpecRepository,
    ICategorySpecRepository categorySpecRepository,
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateCategorySpecCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateCategorySpecCommand request, CancellationToken cancellationToken)
    {
        if (await categoryRepository.GetByIdAsync(request.categoryName) is null)
            return new CategoryNotFoundException(request.categoryName);
        if (await SpecRepository.GetByNameAndCategoryName(request.specName, request.categoryName) is not null)
            return new ConflictException("Conflict.Category.Spec", $"a spec with this name : {request.categoryName} already exists for this category");
        var spec = Specification.Create(request.specName, request.dataType, request.categoryName);
        SpecRepository.Add(spec);
        var categorySpec = CategorySpec.Create(spec.Id, request.categoryName);
        categorySpecRepository.Add(categorySpec);
        await unitOfWork.SaveChangesAsync();
        return spec.Id;
    }
}

internal class CreateCategorySpecCommandValidator : AbstractValidator<CreateCategorySpecCommand>
{
    public CreateCategorySpecCommandValidator()
    {
        RuleFor(c => c.categoryName).NotEmpty();
        RuleFor(c => c.specName).NotEmpty();
        RuleFor(c => c.dataType).NotEmpty().Must(SpecDataType.ValidKey).WithMessage("invalid datatype");
    }
}
