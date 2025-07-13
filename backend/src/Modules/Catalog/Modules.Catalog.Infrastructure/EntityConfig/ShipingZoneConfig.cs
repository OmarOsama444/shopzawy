using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Catalog.Domain.Entities;

namespace Modules.Catalog.Infrastructure.EntityConfig;

public class ShipingZoneConfig : IEntityTypeConfiguration<ShipingZone>
{
    public void Configure(EntityTypeBuilder<ShipingZone> builder)
    {
        builder.HasKey(sz => sz.ShipingZoneName);
    }
}
