using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Users.Domain;

namespace Modules.Users.Infrastructure;

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        builder.HasIndex(u => u.Email)
            .IsUnique();
        builder.Property(u => u.FirstName)
            .IsRequired();
        builder.Property(u => u.LastName)
        .IsRequired();
        builder.Property(u => u.HashedPassword)
            .IsRequired();
        builder.Property(u => u.DateOfCreation)
            .IsRequired();
    }

}
