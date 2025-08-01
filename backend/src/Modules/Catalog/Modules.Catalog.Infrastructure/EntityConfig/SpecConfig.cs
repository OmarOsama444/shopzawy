using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Catalog.Domain.Entities;

namespace Modules.Catalog.Infrastructure.EntityConfig;

public class SpecConfig : IEntityTypeConfiguration<Specification>
{
    public void Configure(EntityTypeBuilder<Specification> builder)
    {
        builder.HasKey(cs => cs.Id);
        builder.HasMany(cs => cs.SpecificationOptions)
            .WithOne(cso => cso.Specification)
            .HasForeignKey(ps => ps.SpecificationId);
        builder.HasMany(cs => cs.CategorySpecs)
            .WithOne(c => c.Specification)
            .HasForeignKey(c => c.SpecId);
    }
}
