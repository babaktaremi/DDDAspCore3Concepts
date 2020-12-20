using System;
using System.Threading.Tasks;
using Domain.Common;
using Domain.OrderAggregate.Events;
using Infrastructure.EventDispatchers.EventDispatchServices.Common;
using Utility;

namespace Infrastructure.EventDispatchers.EventDispatchServices.OrderEventServices
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
