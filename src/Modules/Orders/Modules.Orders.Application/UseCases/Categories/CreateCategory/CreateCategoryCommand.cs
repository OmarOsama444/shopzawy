using System.Data.Common;
using System.Security.Cryptography.X509Certificates;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Modules.Common.Application.Messaging;
using Modules.Common.Application.Validators;
using Modules.Common.Domain;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Application.Services;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Exceptions;
using Modules.Orders.Domain.Repositories;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Application.UseCases.CreateCategory;

public record CreateCategoryCommand(
    int order,
    Guid? parent_category_id,
    ICollection<Guid> spec_ids,
    IDictionary<Language, CategoryLangData> category_data
) : ICommand<Guid>;

public class CreateCategoryCommandHandler(
    ICategoryService categoryService
    ) : ICommandHandler<CreateCategoryCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        return await categoryService.CreateCategory(
            request.order,
            request.parent_category_id,
            request.spec_ids,
            request.category_data);
    }
}

internal class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(c => c.order).NotEmpty();
        RuleFor(c => c.category_data)
            .NotEmpty()
            .Must(x => x.Count == 2)
            .WithMessage("Your Missing the defination for one of the required languages");
        RuleForEach(c => c.category_data)
            .SetValidator(new CategoryLangDataEntryValidator());
    }
}

internal class CategoryLangDataEntryValidator : AbstractValidator<KeyValuePair<Language, CategoryLangData>>
{
    public CategoryLangDataEntryValidator()
    {
        RuleFor(x => x.Key)
            .NotEmpty();
        RuleFor(x => x.Value)
            .NotEmpty()
            .SetValidator(new CategoryLangDataValidator());
    }
}

internal class CategoryLangDataValidator : AbstractValidator<CategoryLangData>
{
    public CategoryLangDataValidator()
    {
        RuleFor(x => x.name).NotEmpty().MinimumLength(3).MaximumLength(100);
        RuleFor(x => x.image_url).NotEmpty().Must(UrlValidator.Must).WithMessage(UrlValidator.Message);
        RuleFor(x => x.description).NotEmpty().MinimumLength(10).MaximumLength(500);
    }
}