using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Catalog.Domain.Entities;

namespace Modules.Catalog.Infrastructure.EntityConfig;

public class SpecOptionConfig : IEntityTypeConfiguration<SpecificationOption>
{
    public void Configure(EntityTypeBuilder<SpecificationOption> builder)
    {
        builder.HasKey(x => new { x.SpecificationId, x.Value });
        builder.HasMany(c => c.ProductItemOptions)
            .WithOne(p => p.SpecificationOptions)
            .HasForeignKey(p => new { p.SpecificationId, p.Value });
    }

}
