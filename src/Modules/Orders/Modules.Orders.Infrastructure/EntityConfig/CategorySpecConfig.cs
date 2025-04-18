using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Orders.Domain.Entities;

namespace Modules.Orders.Infrastructure.EntityConfig;

public class CategorySpecConfig : IEntityTypeConfiguration<CategorySpec>
{
    public void Configure(EntityTypeBuilder<CategorySpec> builder)
    {
        builder.HasKey(x => new { x.CategoryName, x.SpecId });
    }

}
