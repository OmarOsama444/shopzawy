using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Catalog.Domain.Entities;

namespace Modules.Catalog.Infrastructure.EntityConfig;

public class CategoryConfig : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasMany(c => c.ChilrenCategories)
        .WithOne(c => c.ParentCategory)
        .HasForeignKey(c => c.ParentCategoryId)
        .IsRequired(false);

        builder.HasMany(c => c.CategorySpecs)
        .WithOne(s => s.Category)
        .HasForeignKey(s => s.CategoryId);

        builder.HasMany(c => c.Products)
        .WithOne(p => p.Category)
        .HasForeignKey(p => p.CategoryId);
        builder
        .Property(c => c.Path)
        .HasConversion(
            v =>  string.Join("." , v )  ,
            v => v.Split('.', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList()
        )
        .HasColumnType("TEXT");
        builder
            .HasData(
                Category.Seed()
            );
    }
}
