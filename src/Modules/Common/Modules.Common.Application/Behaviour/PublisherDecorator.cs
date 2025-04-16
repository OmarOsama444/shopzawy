using System.Reflection;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Modules.Common.Application.Behaviour;

public class PublisherDecorator(
    IPublisher innerPublisher,
    ILogger<PublisherDecorator> logger) : IPublisher
{
    public async Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
        where TNotification : INotification
    {
        logger.LogInformation("Before publishing notification");
        await innerPublisher.Publish(notification, cancellationToken);
        logger.LogInformation("After publishing notification");
    }

    public async Task Publish(object notification, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Before publishing notification");
        await innerPublisher.Publish(notification, cancellationToken);
        logger.LogInformation("After publishing notification");
    }

}

