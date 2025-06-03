using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Modules.Users.Domain;
using Modules.Users.Domain.Entities;

namespace Modules.Users.Infrastructure.Services;

public static class DatabaseSeedService
{
    public static async Task SeedAsync(
        UserDbContext userDbContext,
        UserManager<User> userManager,
        RoleManager<Role> roleManager)
    {
        await userDbContext.Database.MigrateAsync();

        if (await userDbContext.Users.AnyAsync())
        {
            return;
        }

        var roles = new[] { "Admin", "Member" };

        foreach (var roleName in roles)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(Role.Create(roleName));
            }
        }

        Role? role = await roleManager.Roles.FirstOrDefaultAsync(r => r.Name == "Admin");
        if (role is not null)
            await roleManager.AddClaimAsync(role, new Claim("Permission", "Role.Manage"));

        var adminEmail = "omarosama4buss@gmail.com";
        var adminUser = User.Create(
            "System",
            "Owner",
            adminEmail,
            "+201055449675"
        );

        adminUser.EmailConfirmed = true;
        adminUser.PhoneNumberConfirmed = true;

        var userExists = await userManager.FindByEmailAsync(adminEmail);
        if (userExists == null)
        {
            var result = await userManager.CreateAsync(adminUser, "Admin123");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
            else
            {
                throw new Exception("Failed to create admin user: " +
                    string.Join("; ", result.Errors.Select(e => e.Description)));
            }
        }

    }
}
