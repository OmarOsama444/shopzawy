using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Users.Domain.Entities;

namespace Modules.Users.Infrastructure.EntityConfig;

public class UnverifiedUserConfig : IEntityTypeConfiguration<UnverifiedUser>
{
    public void Configure(EntityTypeBuilder<UnverifiedUser> builder)
    {
        builder.HasIndex(e => e.Email);
    }
}
