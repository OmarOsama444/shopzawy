using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Modules.Users.Domain;
using Modules.Users.Domain.Entities;

namespace Modules.Users.Infrastructure.Services;

public static class DatabaseSeedService
{
    public static async Task SeedAsync(
        UsersDbContext userDbContext)
    {
        await userDbContext.Database.MigrateAsync();
    }
}
