using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Orders.Domain.Entities;

namespace Modules.Orders.Infrastructure.EntityConfig;

public class CategoryConfig : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasMany(c => c.ChilrenCategories)
        .WithOne(c => c.ParentCategory)
        .HasForeignKey(c => c.ParentCategoryId)
        .IsRequired(false);

        builder.HasIndex(c => c.Order)
            .IsUnique();

        builder.HasMany(c => c.CategorySpecs)
        .WithOne(s => s.Category)
        .HasForeignKey(s => s.CategoryId);

        builder.HasMany(c => c.Products)
        .WithOne(p => p.Category)
        .HasForeignKey(p => p.CategoryId);

    }
}
