using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Catalog.Domain.Entities;

namespace Modules.Catalog.Infrastructure.EntityConfig;

public class CategorySpecConfig : IEntityTypeConfiguration<CategorySpec>
{
    public void Configure(EntityTypeBuilder<CategorySpec> builder)
    {
        builder
            .HasKey(x => x.Id);
        builder
            .HasOne(x => x.Category)
            .WithMany(x => x.CategorySpecs)
            .HasForeignKey(x => x.CategoryId);
        builder
            .HasOne(x => x.Specification)
            .WithMany(x => x.CategorySpecs)
            .HasForeignKey(x => x.SpecId);
    }

}
