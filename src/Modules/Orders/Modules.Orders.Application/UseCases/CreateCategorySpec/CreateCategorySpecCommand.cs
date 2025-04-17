
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
    ISpecRepository specRepository,
    ICategorySpecRepository categorySpecRepository,
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork
) : ICommandHandler<CreateCategorySpecCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateCategorySpecCommand request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetByIdAsync(request.categoryName);
        if (category is null)
            return new CategoryNotFoundException(request.categoryName);

        var existingSpec = await specRepository.GetByNameAndCategoryName(request.specName, request.categoryName);
        if (existingSpec is not null)
            return new ConflictException("Conflict.Category.Spec",
                $"A spec with this name: {request.specName} already exists for this category.");

        var spec = Specification.Create(request.specName, request.dataType, request.categoryName);
        specRepository.Add(spec);

        var categorySpec = CategorySpec.Create(spec.Id, request.categoryName);
        categorySpecRepository.Add(categorySpec);

        var childCategories = await categoryRepository.Children(request.categoryName);
        foreach (var child in childCategories)
        {
            var childSpec = CategorySpec.Create(spec.Id, child.CategoryName);
            categorySpecRepository.Add(childSpec);
        }

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
