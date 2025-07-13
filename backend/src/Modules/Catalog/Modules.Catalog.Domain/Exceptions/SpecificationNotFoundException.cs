using Common.Domain.Exceptions;

namespace Modules.Catalog.Domain.Exceptions;

public class SpecificationNotFoundException : NotFoundException
{
    public SpecificationNotFoundException(Guid id) : base("Spec.NotFound",
    $"Spec with id {id} not found")
    {

    }
}
