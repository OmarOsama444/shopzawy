using Common.Domain.Exceptions;

namespace Modules.Catalog.Domain.Exceptions;

public class CategoryNameConflictException : ConflictException
{
    public CategoryNameConflictException(string name) : base("Category.Conflict", $"a category with this name : {name} already exists")
    {
    }
}
