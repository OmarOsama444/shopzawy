using FluentValidation;
using Modules.Common.Application.Messaging;
using Modules.Common.Application.Validators;
using Modules.Common.Domain;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Exceptions;
using Modules.Orders.Domain.Repositories;

namespace Modules.Orders.Application.UseCases.CreateCategory;

public record CreateCategoryCommand(
    string CategoryName,
    string Description,
    int Order,
    string ImageUrl,
    string? ParentCategoryName,
    ICollection<Guid> Ids
) : ICommand<string>;

public class CreateCategoryCommandHandler(
    ICategoryRepository categoryRepository,
    ICategorySpecRepositroy categorySpecRepositroy,
    ISpecRepository specRepository, IUnitOfWork unitOfWork) : ICommandHandler<CreateCategoryCommand, string>
{
    public async Task<Result<string>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        if (await categoryRepository.GetByIdAsync(request.CategoryName) is not null)
            return new CategoryNameConflictException(request.CategoryName);
        Category? parentCategory = null;
        if (!String.IsNullOrEmpty(request.ParentCategoryName))
        {
            parentCategory = await categoryRepository.GetByIdAsync(request.ParentCategoryName);
            if (parentCategory is null)
                return new CategoryNotFoundException(request.ParentCategoryName);
        }
        var category = Category.Create(
                request.CategoryName,
                request.Description,
                request.Order,
                request.ImageUrl,
                parentCategory
            );
        categoryRepository.Add(category);
        foreach (var id in request.Ids)
        {
            var spec = await specRepository.GetByIdAsync(id);
            if (spec is null)
                return new SpecificationNotFoundException(id);
            var categorySpec = CategorySpec.Create(request.CategoryName, id);
            categorySpecRepositroy.Add(categorySpec);
        }
        await unitOfWork.SaveChangesAsync();
        return request.CategoryName;
    }
}

internal class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(c => c.CategoryName).NotEmpty().MinimumLength(3).MaximumLength(100);
        RuleFor(c => c.Description).NotEmpty().MinimumLength(3);
        RuleFor(c => c.Order).NotEmpty();
        RuleFor(c => c.ImageUrl)
            .NotEmpty()
            .Must(UrlValidator.Must)
            .WithMessage(UrlValidator.Message);
    }
}