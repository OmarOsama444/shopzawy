using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Orders.Domain.Entities;

namespace Modules.Orders.Infrastructure.EntityConfig;

public class CategoryConfig : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.CategoryName);

        builder.Property(c => c.CategoryName)
        .IsRequired()
        .HasMaxLength(100);

        builder.HasMany(c => c.ChilrenCategories)
        .WithOne(c => c.ParentCategory)
        .HasForeignKey(c => c.ParentCategoryName)
        .IsRequired(false);

        builder.HasMany(c => c.CategorySpecs)
        .WithOne(s => s.Category)
        .HasForeignKey(s => s.CategoryName);


        builder.HasMany(c => c.Products)
        .WithOne(p => p.MainCategory)
        .HasForeignKey(p => p.CategoryName);

        builder.HasMany(s => s.ProductCategories)
            .WithOne(pc => pc.Category)
            .HasForeignKey(pc => pc.CategoryName);
    }
}
