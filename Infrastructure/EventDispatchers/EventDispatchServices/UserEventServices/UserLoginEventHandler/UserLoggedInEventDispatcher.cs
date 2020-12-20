using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Common;
using Domain.UserAggregate.Events;
using Infrastructure.EventDispatchers.EventDispatchServices.Common;
using Utility;

namespace Infrastructure.EventDispatchers.EventDispatchServices.UserEventServices.UserLoginEventHandler
{
   public class UserLoggedInEventDispatcher:IEventDispatcher,IScopedDependency
    {
        readonly IUserLoginChannel _userLoginChannel;

        public UserLoggedInEventDispatcher(IUserLoginChannel userLoginChannel)
        {
            _userLoginChannel = userLoginChannel;
        }

        public async Task Handle(IDomainEvent domainEvent)
        {
            if (domainEvent is UserLoginEvent e)
                await _userLoginChannel.AddToChannelAsync(e.UserId, CancellationToken.None);
        }
    }
}
