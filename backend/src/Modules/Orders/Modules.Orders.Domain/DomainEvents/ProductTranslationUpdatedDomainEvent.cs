using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Domain.DomainEvent;

namespace Modules.Orders.Domain.DomainEvents
{
    public class ProductTranslationUpdatedDomainEvent : DomainEvent
    {
        public Guid ProductId { get; set; }
        public ProductTranslationUpdatedDomainEvent(Guid ProductId)
        {
            this.ProductId = ProductId;
        }
    }
}