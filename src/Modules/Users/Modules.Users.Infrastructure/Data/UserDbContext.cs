using Microsoft.EntityFrameworkCore;
using Modules.Common.Infrastructure;
using Modules.Common.Infrastructure.Inbox;
using Modules.Common.Infrastructure.Outbox;
using Modules.Users.Domain.Entities;
using Modules.Users.Infrastructure.EntityConfig;

namespace Modules.Users.Infrastructure;

public class UsersDbContext(DbContextOptions<UsersDbContext> Options) :
    DbContext(Options)
{
    public DbSet<User> users { get; set; }
    public DbSet<Token> tokens { get; set; }
    public DbSet<Role> roles { get; set; }
    public DbSet<Permission> permissions { get; set; }
    public DbSet<RolePermission> rolePermissions { get; set; }
    public IQueryable<RolePermission> RolePermissions => this.rolePermissions;
    public DbSet<UserRole> userRoles { get; set; }
    public IQueryable<UserRole> UserRoles => this.userRoles;
    public DbSet<OutboxMessage> outboxMessages { get; set; }
    public DbSet<OutboxConsumerMessage> outboxConsumerMessages { get; set; }
    public DbSet<InboxMessage> inboxMessages { get; set; }
    public DbSet<InboxConsumerMessage> inboxConsumerMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Users);
        modelBuilder.ApplyConfiguration<User>(new UserConfig());
        modelBuilder.ApplyConfiguration<Token>(new TokenConfig());
        modelBuilder.ApplyConfiguration<Permission>(new PermissionConfig());
        modelBuilder.ApplyConfiguration<Role>(new RoleConfig());
        modelBuilder.ApplyConfiguration<UserRole>(new UserRoleConfig());
        modelBuilder.ApplyConfiguration<RolePermission>(new RolePermissionConfig());

        // outbox-inbox messages config
        modelBuilder.ApplyConfiguration<OutboxMessage>(new OutboxMessageConfiguration());
        modelBuilder.ApplyConfiguration<OutboxConsumerMessage>(new OutboxConsumerMessageConfiguration());
        modelBuilder.ApplyConfiguration<InboxMessage>(new InboxMessageConfiguration());
        modelBuilder.ApplyConfiguration<InboxConsumerMessage>(new InboxConsumerMessageConfiguration());

    }


}
