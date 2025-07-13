using Common.Domain.Exceptions;

namespace Modules.Catalog.Domain.Exceptions;

public class CategoryNotFoundException : NotFoundException
{
    public CategoryNotFoundException(Guid categoryId) :
        base(
            "Category.NotFound",
            $"Category with name {categoryId} not found")
    {
    }
}
