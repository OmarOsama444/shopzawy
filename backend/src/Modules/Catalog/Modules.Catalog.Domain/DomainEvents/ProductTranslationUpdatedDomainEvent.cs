using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Domain.DomainEvent;

namespace Modules.Catalog.Domain.DomainEvents
{
    public class ProductTranslationUpdatedDomainEvent(Guid ProductId) : DomainEvent
    {
        public Guid ProductId { get; set; } = ProductId;
    }
}