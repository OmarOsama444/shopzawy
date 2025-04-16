using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Common.Application.EventBus;
public interface IIntegrationEvent
{
    Guid id { get; }
    DateTime OccuredAt { get; }
}
