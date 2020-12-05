using System;
using System.Threading.Tasks;
using Domain.Common;

namespace Infrastructure.EventDispatchers.EventDispatchServices.Common
{
    public interface IEventDispatcher
    {
        Task Handle(IDomainEvent domainEvent);
    }
}
