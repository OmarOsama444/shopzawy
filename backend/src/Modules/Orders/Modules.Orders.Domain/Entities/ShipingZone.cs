using Common.Domain.Entities;

namespace Modules.Orders.Domain.Entities;

public class ShipingZone : Entity
{
    public string ShipingZoneName { get; set; } = string.Empty;
    public virtual ICollection<Vendor> Vendors { get; set; } = [];
}
