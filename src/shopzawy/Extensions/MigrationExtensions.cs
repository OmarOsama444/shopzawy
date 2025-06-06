using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Modules.Orders.Infrastructure.Data;
using Modules.Users.Domain;
using Modules.Users.Domain.Entities;
using Modules.Users.Infrastructure;
using Modules.Users.Infrastructure.Services;

namespace shopzawy.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();
        ApplyMigration<OrdersDbContext>(scope);
        ApplyMigration<UsersDbContext>(scope);

        // Run seed logic for UserDbContext
        // var userDbContext = scope.ServiceProvider.GetRequiredService<UsersDbContext>();
        // var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        // var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
        // await DatabaseSeedService.SeedAsync(userDbContext, userManager, roleManager);
    }

    private static void ApplyMigration<TDbContext>(IServiceScope scope)
        where TDbContext : DbContext
    {
        using TDbContext context = scope.ServiceProvider.GetRequiredService<TDbContext>();
        context.Database.Migrate();
    }
}
