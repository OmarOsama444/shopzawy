using Modules.Common.Domain.Exceptions;

namespace Modules.Orders.Domain.Exceptions;

public class CategoryNotFoundException : NotFoundException
{
    public CategoryNotFoundException(string CategoryName) :
        base(
            "Category.NotFound",
            $"Category with name {CategoryName} not found")
    {
    }
}

public class SpecificationNotFoundException : NotFoundException
{
    public SpecificationNotFoundException(Guid id) : base("Spec.NotFound",
    $"Spec with id {id} not found")
    {

    }
}