using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;
using Domain.OrderAggregate.Events;
using Infrastructure.EventDispatchers.EventDispatchServices;
using Utility;

namespace Infrastructure.EventDispatchers.EventDispatchHandlers
{
    public interface IEventDispatchHandler
    {
        void HandleEvents(IEnumerable<IDomainEvent> domainEvents);
    }

    public class EventDispatchHandler:IScopedDependency, IEventDispatchHandler
    {
       private readonly IConsoleEventDispatcher _consoleEventDispatcher;

       public EventDispatchHandler(IConsoleEventDispatcher consoleEventDispatcher)
       {
           _consoleEventDispatcher = consoleEventDispatcher;
       }


       public void HandleEvents(IEnumerable<IDomainEvent> domainEvents)
       {
           foreach (var domainEvent in domainEvents)
           {
               DispatchEvents(domainEvent);
           }
       }

       private void DispatchEvents(IDomainEvent domainEvent)
       {
           switch (domainEvent)
           {
                case OrderCanceledEvent orderCanceled:
                    _consoleEventDispatcher.WriteToConsole(orderCanceled.ToString());
                    break;

                default:
                    throw new ArgumentException("Event Not Recognized");
           }
       }
   }
}
