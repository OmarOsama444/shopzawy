using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Orders.Domain.Entities;

namespace Modules.Orders.Infrastructure.EntityConfig;

public class SpecConfig : IEntityTypeConfiguration<Specification>
{
    public void Configure(EntityTypeBuilder<Specification> builder)
    {
        builder.HasKey(cs => cs.Id);
        builder.HasMany(cs => cs.SpecificationOptions)
            .WithOne(cso => cso.Specification)
            .HasForeignKey(ps => ps.SpecificationId);
        builder.HasIndex(cs => cs.Name);
        builder.HasMany(cs => cs.CategorySpecs)
            .WithOne(c => c.Spec)
            .HasForeignKey(c => c.SpecId);
    }
}
