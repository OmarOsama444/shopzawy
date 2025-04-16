
using FluentValidation;
using MassTransit.Topology;
using Modules.Common.Application.Messaging;
using Modules.Common.Application.Validators;
using Modules.Common.Domain;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Exceptions;
using Modules.Orders.Domain.Repositories;

namespace Modules.Orders.Application.UseCases.UpdateCategory;

public record UpdateCategoryCommand(string categoryName, string? Description, int? Order, string? ImageUrl) : ICommand;

public sealed class UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork) : ICommandHandler<UpdateCategoryCommand>
{
    public async Task<Result> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        Category? category = await categoryRepository.GetByIdAsync(request.categoryName);
        if (category is null)
            return new CategoryNotFoundException(request.categoryName);
        category.Update(request.Description, request.Order, request.ImageUrl);
        categoryRepository.Update(category);
        await unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}

internal class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(c => c.categoryName).NotEmpty();
        RuleFor(c => c.ImageUrl!)
            .Must(UrlValidator.Must)
            .WithMessage(UrlValidator.Message)
            .When(c => !String.IsNullOrEmpty(c.ImageUrl));
    }
}

