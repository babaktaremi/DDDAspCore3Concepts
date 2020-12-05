using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;
using Domain.OrderAggregate.Events;
using Infrastructure.EventDispatchers.EventDispatchHandlers;
using Infrastructure.EventDispatchers.EventDispatchServices.Common;
using Microsoft.Extensions.Logging;
using Utility;

namespace Infrastructure.EventDispatchers.EventDispatchServices
{
   public class OrderRegisteredEventDispatcher:IEventDispatcher,IScopedDependency
   {
       private readonly ILogger<OrderRegisteredEventDispatcher> _logger;

       public OrderRegisteredEventDispatcher(ILogger<OrderRegisteredEventDispatcher> logger)
       {
           _logger = logger;
       }

       public Task Handle(IDomainEvent domainEvent)
        {
           if(domainEvent is OrderRegisteredEvent e)
               _logger.LogWarning("User With {userId} Registered an order at {date}",e.UserId,e.DateTime);

           return Task.CompletedTask;
        }
    }
}
