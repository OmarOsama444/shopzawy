using Common.Application.Messaging;
using Common.Domain;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Application.Repositories;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Exceptions;
using Modules.Orders.Domain.Repositories;

namespace Modules.Orders.Application.UseCases.Categories.UpdateCategorySpec;

public class UpdateCategorySpecCommandHandler(
    ICategorySpecRepositroy categorySpecRepositroy,
    ICategoryRepository categoryRepository,
    ISpecRepository specRepository,
    IUnitOfWork unitOfWork
) : ICommandHandler<UpdateCategorySpecCommand>
{
    public async Task<Result> Handle(UpdateCategorySpecCommand request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetByIdAsync(request.CategoryId);
        if (category is null)
            return new CategoryNotFoundException(request.CategoryId);
        foreach (Guid specId in request.Add)
        {
            var specification = await specRepository.GetByIdAsync(specId);
            if (specification is null)
                return new SpecificationNotFoundException(specId);
            if (await categorySpecRepositroy.GetByCategoryIdAndSpecId(request.CategoryId, specId) is null)
            {
                var categorySpec = CategorySpec.Create(request.CategoryId, specId);
                categorySpecRepositroy.Add(categorySpec);
            }
        }
        foreach (Guid specId in request.Remove)
        {
            var specification = await specRepository.GetByIdAsync(specId);
            if (specification is null)
                return new SpecificationNotFoundException(specId);
            var categorySpec =
                await
                    categorySpecRepositroy
                    .GetByCategoryIdAndSpecId(request.CategoryId, specId);
            if (categorySpec is not null)
            {
                categorySpecRepositroy.Remove(categorySpec);
            }
        }
        await unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}
