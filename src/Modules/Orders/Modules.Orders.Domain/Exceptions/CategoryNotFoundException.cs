using Modules.Common.Domain.Exceptions;

namespace Modules.Orders.Domain.Exceptions;

public class CategoryNotFoundException : NotFoundException
{
    public CategoryNotFoundException(Guid categoryId) :
        base(
            "Category.NotFound",
            $"Category with name {categoryId} not found")
    {
    }
}
