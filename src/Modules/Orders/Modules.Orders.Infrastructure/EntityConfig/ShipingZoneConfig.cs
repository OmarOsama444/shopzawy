using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Orders.Domain.Entities;

namespace Modules.Orders.Infrastructure.EntityConfig;

public class ShipingZoneConfig : IEntityTypeConfiguration<ShipingZone>
{
    public void Configure(EntityTypeBuilder<ShipingZone> builder)
    {
        builder.HasKey(sz => sz.ShipingZoneName);
    }
}
