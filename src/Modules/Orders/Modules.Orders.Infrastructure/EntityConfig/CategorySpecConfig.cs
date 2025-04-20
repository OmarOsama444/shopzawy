using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Orders.Domain.Entities;

namespace Modules.Orders.Infrastructure.EntityConfig;

public class CategorySpecConfig : IEntityTypeConfiguration<CategorySpec>
{
    public void Configure(EntityTypeBuilder<CategorySpec> builder)
    {
        builder
            .HasKey(x => x.Id);
        builder
            .HasOne(x => x.Category)
            .WithMany(x => x.CategorySpecs)
            .HasForeignKey(x => x.CategoryName);
        builder
            .HasOne(x => x.Specification)
            .WithMany(x => x.CategorySpecs)
            .HasForeignKey(x => x.SpecId);
    }

}
