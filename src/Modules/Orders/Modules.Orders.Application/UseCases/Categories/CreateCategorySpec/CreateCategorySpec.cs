using FluentValidation;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Exceptions;
using Modules.Orders.Domain.Repositories;

namespace Modules.Orders.Application.UseCases.Categories.CreateCategorySpec;

public record CreateCategorySpecCommand(Guid category_id, ICollection<Guid> spec_ids) : ICommand;

public class CreateCategorySpecCommandHandler(
    ICategorySpecRepositroy categorySpecRepositroy,
    ICategoryRepository categoryRepository,
    ISpecRepository specRepository,
    IUnitOfWork unitOfWork
) : ICommandHandler<CreateCategorySpecCommand>
{
    public async Task<Result> Handle(CreateCategorySpecCommand request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetByIdAsync(request.category_id);
        if (category is null)
            return new CategoryNotFoundException(request.category_id);
        foreach (var spec_id in request.spec_ids)
        {
            var specification = await specRepository.GetByIdAsync(spec_id);
            if (specification is null)
                return new SpecificationNotFoundException(spec_id);
            var categorySpec = CategorySpec.Create(request.category_id, spec_id);
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
        RuleFor(x => x.category_id).NotEmpty();
        RuleFor(x => x.spec_ids).NotEmpty();
    }
}