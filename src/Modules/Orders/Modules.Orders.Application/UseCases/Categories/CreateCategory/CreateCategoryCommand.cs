using System.Data.Common;
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
    ICategoryService categoryService
    ) : ICommandHandler<CreateCategoryCommand, string>
{
    public async Task<Result<string>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        return await categoryService.CreateCategory(
            request.CategoryName,
            request.Description,
            request.Order,
            request.ImageUrl,
            request.ParentCategoryName,
            request.Ids);
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