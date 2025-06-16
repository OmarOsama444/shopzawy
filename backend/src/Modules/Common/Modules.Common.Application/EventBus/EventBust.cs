using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Common.Application.EventBus
{
    public class EventBus(IBus bus) : IEventBus
    {
        public async Task PublishAsync<T>(T IntegrationEvent, CancellationToken token = default) where T : IIntegrationEvent
        {
            await bus.Publish(IntegrationEvent, token);
        }
    }
}
