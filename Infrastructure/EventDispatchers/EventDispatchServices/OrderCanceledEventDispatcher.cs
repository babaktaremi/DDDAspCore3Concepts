using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;
using Domain.OrderAggregate.Events;
using Infrastructure.EventDispatchers.EventDispatchServices.Common;
using Utility;

namespace Infrastructure.EventDispatchers.EventDispatchServices
{
   public class OrderCanceledEventDispatcher:IScopedDependency,IEventDispatcher
    {
        public Task Handle(IDomainEvent domainEvent)
        {
            if(domainEvent is OrderCanceledEvent e)
                Console.WriteLine(e.ToString());

            return Task.CompletedTask;
        }
    }
}
