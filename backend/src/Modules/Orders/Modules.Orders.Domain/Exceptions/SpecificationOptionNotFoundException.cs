using Common.Domain.Exceptions;

namespace Modules.Orders.Domain.Exceptions;

public class SpecificationOptionNotFoundException : NotFoundException
{
    public SpecificationOptionNotFoundException(Guid id, string value) : base("Spec.Option.NotFound",
    $"Spec Option with id {id} and value {value} ")
    { }
}