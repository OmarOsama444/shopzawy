using System.Data.Common;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
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
    IProductRepository productRepository
    , IUnitOfWork unitOfWork) : ICommandHandler<CreateCategoryCommand, string>
{
    public async Task<Result<string>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        if (await categoryRepository.GetByIdAsync(request.CategoryName) is not null)
            return new CategoryNameConflictException(request.CategoryName);
        await unitOfWork.BeginTransactionAsync();
        try
        {
            Category? parentCategory = null;
            HashSet<Guid> specIds = new(request.Ids);
            var category = Category.Create(
                    request.CategoryName,
                    request.Description,
                    request.Order,
                    request.ImageUrl,
                    parentCategory
                );
            categoryRepository.Add(category);
            await unitOfWork.SaveChangesAsync();
            if (!String.IsNullOrEmpty(request.ParentCategoryName))
            {
                parentCategory = await categoryRepository.GetByIdAsync(request.ParentCategoryName);
                if (parentCategory is null)
                    return new CategoryNotFoundException(request.ParentCategoryName);
                var parentSpecIds = parentCategory.CategorySpecs.Select(x => x.SpecId);
                foreach (var id in parentSpecIds)
                {
                    specIds.Add(id);
                }

                await productRepository
                    .UpdateCategoryName(
                        request.ParentCategoryName,
                        request.CategoryName
                        );

                await categorySpecRepositroy
                    .DeleteCategoryName(
                        request.ParentCategoryName
                    );
            }
            foreach (var id in specIds)
            {
                var categorySpec = CategorySpec.Create(request.CategoryName, id);
                categorySpecRepositroy.Add(categorySpec);
            }
            await unitOfWork.SaveChangesAsync();
            await unitOfWork.CommitTransactionAsync();
        }
        catch (Exception ex)
        {
            await unitOfWork.RollBackTransactionAsync();
            return ex;
        }
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