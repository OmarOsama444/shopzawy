using Modules.Common.Application.Messaging;
using Modules.Common.Domain.Exceptions;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Domain.DomainEvents;
using Modules.Orders.Domain.Repositories;

namespace Modules.Orders.Application.UseCases.CreateCategory;

public class CagtegoryCreatedDomainEventHandler(
    ICategoryRepository categoryRepository,
    ICategorySpecRepositroy categorySpecRepositroy,
    IProductRepository productRepository,
    IUnitOfWork unitOfWork
) : IDomainEventHandler<CategoryCreatedDomainEvent>
{
    public async Task Handle(CategoryCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetByIdAsync(notification.CategoryName);
        if (category is null)
            throw new SkillHiveException($"Category with id {notification.CategoryName} null at {nameof(CagtegoryCreatedDomainEventHandler)}");
        if (category.ParentCategoryName is null)
            return;
        await unitOfWork.BeginTransactionAsync();
        try
        {
            await categorySpecRepositroy.UpdateCategoryName(category.ParentCategoryName, category.CategoryName);
            await productRepository.UpdateCategoryName(category.ParentCategoryName, category.CategoryName);

            await unitOfWork.SaveChangesAsync();
            await unitOfWork.CommitTransactionAsync();
        }
        catch
        {
            await unitOfWork.RollBackTransactionAsync();
            throw;
        }
    }

}
