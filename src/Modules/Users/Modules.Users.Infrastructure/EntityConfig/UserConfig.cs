using Markdig.Extensions.Yaml;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Users.Domain;

namespace Modules.Users.Infrastructure;

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.FirstName)
            .IsRequired();
        builder.Property(u => u.LastName)
            .IsRequired();
        builder.Property(u => u.DateOfCreation)
            .IsRequired();
        builder.HasIndex(u => u.ConfirmationToken);

        builder.HasMany(e => e.UserTokens)
            .WithOne(e => e.User)
            .HasForeignKey(ut => ut.UserId)
            .IsRequired();

        builder.HasMany(e => e.UserRoles)
            .WithOne(e => e.User)
            .HasForeignKey(ur => ur.UserId)
            .IsRequired();
    }

}
