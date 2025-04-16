using System.Runtime.InteropServices;
using Modules.Common.Domain.Entities;
using Modules.Orders.Domain.DomainEvents;

namespace Modules.Orders.Domain.Entities;
// TODO
//when spec created it adds to all the children
//when deleted it deletes from parents
public class Specification : Entity
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string DataType { get; private set; } = string.Empty;
    public virtual ICollection<SpecificationOption> SpecificationOptions { get; set; } = [];
    public virtual ICollection<CategorySpec> CategorySpecs { get; set; } = [];
    public static Specification Create(string name, string dataType, string CategoryName)
    {
        var cat = new Specification() { Id = Guid.NewGuid(), Name = name, DataType = dataType };
        cat.RaiseDomainEvent(new SpecCreatedDomainEvent(cat.Id));
        return cat;
    }
}
