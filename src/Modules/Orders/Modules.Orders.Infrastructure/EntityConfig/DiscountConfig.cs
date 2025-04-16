using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Orders.Domain.Entities;

namespace Modules.Orders.Infrastructure.EntityConfig;

public class DiscountConfig : IEntityTypeConfiguration<Discount>
{
    public void Configure(EntityTypeBuilder<Discount> builder)
    {
        builder.HasKey(d => d.Id);
        builder.HasIndex(d => d.ExpiryDate);
        builder
            .HasMany(d => d.ProductDiscounts)
            .WithOne(pd => pd.Discount)
            .HasForeignKey(pd => pd.DiscountId);
    }
}
