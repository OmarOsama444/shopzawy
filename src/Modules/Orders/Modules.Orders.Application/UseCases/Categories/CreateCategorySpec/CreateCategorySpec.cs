using FluentValidation;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Exceptions;
using Modules.Orders.Domain.Repositories;

namespace Modules.Orders.Application.UseCases.Categories.CreateCategorySpec;

public record CreateCategorySpecCommand(string categoryName, ICollection<Guid> specIds) : ICommand;

public class CreateCategorySpecCommandHandler(
    ICategorySpecRepositroy categorySpecRepositroy,
    ICategoryRepository categoryRepository,
    ISpecRepository specRepository,
    IUnitOfWork unitOfWork
) : ICommandHandler<CreateCategorySpecCommand>
{
    public async Task<Result> Handle(CreateCategorySpecCommand request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetByIdAsync(request.categoryName);
        if (category is null)
            return new CategoryNotFoundException(request.categoryName);
        foreach (var specId in request.specIds)
        {
            var specification = await specRepository.GetByIdAsync(specId);
            if (specification is null)
                return new SpecificationNotFoundException(specId);
            var categorySpec = CategorySpec.Create(request.categoryName, specId);
            categorySpecRepositroy.Add(categorySpec);
        }
        await unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}

internal class CreateCategorySpecCommandValidator : AbstractValidator<CreateCategorySpecCommand>
{
    public CreateCategorySpecCommandValidator()
    {
        RuleFor(x => x.categoryName).NotEmpty();
        RuleFor(x => x.specIds).NotEmpty();
    }
}