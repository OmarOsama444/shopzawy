using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Infrastructure.Data;
using Modules.Orders.Infrastructure.Elastic;
using Modules.Users.Infrastructure;
using Nest;

namespace shopzawy.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();
        ApplyMigration<OrdersDbContext>(scope);
        ApplyMigration<UsersDbContext>(scope);
        var elasticClientFactory = scope.ServiceProvider.GetRequiredService<IElasticClientFactory>();
        var elasticClient = elasticClientFactory.CreateElasticClient();
        ElasticSearchIndexIntializer.InitializeElasticSearchIndex(elasticClient);
    }

    private static void ApplyMigration<TDbContext>(IServiceScope scope)
        where TDbContext : DbContext
    {
        using TDbContext context = scope.ServiceProvider.GetRequiredService<TDbContext>();
        context.Database.Migrate();
    }
}
