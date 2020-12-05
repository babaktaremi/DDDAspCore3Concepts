using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;
using Domain.OrderAggregate.Events;
using Infrastructure.EventDispatchers.EventDispatchServices;
using Infrastructure.EventDispatchers.EventDispatchServices.Common;
using Utility;

namespace Infrastructure.EventDispatchers.EventDispatchHandlers
{
    public interface IEventDispatchHandler
    {
        Task HandleEvents(IEnumerable<IDomainEvent> domainEvents);
    }

    public class EventDispatchHandler:IScopedDependency, IEventDispatchHandler
    {
       private readonly IEnumerable<IEventDispatcher> _eventDispatcher;

       public EventDispatchHandler(IEnumerable<IEventDispatcher> eventDispatcher)
       {
           _eventDispatcher = eventDispatcher;
       }


       public async Task HandleEvents(IEnumerable<IDomainEvent> domainEvents)
       {
           foreach (var domainEvent in domainEvents)
           {
             await  DispatchEvents(domainEvent);
           }
       }

       private async Task DispatchEvents(IDomainEvent domainEvent)
       {
           foreach (var eventDispatcher in _eventDispatcher)
           {
              await eventDispatcher.Handle(domainEvent);
           }
       }
   }
}
