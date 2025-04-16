using System.Runtime.CompilerServices;
using Modules.Common.Domain.Entities;

namespace Modules.Orders.Domain.Entities;

public class Discount : Entity
{
    public Guid Id { get; private set; }
    public float? Precentage { get; private set; }
    public float? Value { get; private set; }
    public DateTime ExpiryDate { get; private set; }
    public virtual ICollection<ProductDiscount> ProductDiscounts { get; set; } = [];
}
