using Common.Domain.DomainEvent;
using Common.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Common.Infrastructure.interceptors
{
    public class PublishDomainEventsInterceptors(IServiceProvider serviceProvider, ILogger<PublishDomainEventsInterceptors> logger) : SaveChangesInterceptor
    {
        public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            if (eventData.Context != null)
            {
                await PublishDomainEventsAsync(eventData.Context);
            }
            return await base.SavedChangesAsync(eventData, result, cancellationToken);
        }

        private async Task PublishDomainEventsAsync(DbContext context)
        {
            var domainEvents = context
                .ChangeTracker
                .Entries<Entity>()
                .Select(entry => entry.Entity)
                .SelectMany(entity =>
                {
                    IReadOnlyCollection<IDomainEvent> domainEvents = entity.DomainEvents;
                    entity.ClearDomainEvents();
                    return domainEvents;
                })
                .ToList();
            using IServiceScope scope = serviceProvider.CreateScope();
            IPublisher publisher = scope.ServiceProvider.GetRequiredService<IPublisher>();
            foreach (var domainEvent in domainEvents)
            {
                logger.LogInformation("Domain event raised {domainEvent}", JsonSerializer.Serialize(domainEvent));
                await publisher.Publish(domainEvent);
            }
        }
    }
}
