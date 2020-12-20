using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Utility;

namespace Infrastructure.EventDispatchers.EventDispatchServices.UserEventServices.UserLoginEventHandler
{
   public interface IUserLoginChannel
   {
       Task AddToChannelAsync(int userId, CancellationToken cancellationToken);
        IAsyncEnumerable<int> ReturnValue(CancellationToken cancellationToken);
   }

   public class UserLoginChannel : IUserLoginChannel, ISingletonDependency
   {
       private Channel<int> serviceChannel;

       public UserLoginChannel()
       {
           serviceChannel = Channel.CreateBounded<int>(new BoundedChannelOptions(4000)
           {
               SingleReader = false,
               SingleWriter = false
           });
       }


        public async Task AddToChannelAsync(int userId, CancellationToken cancellationToken)
       {
           await serviceChannel.Writer.WriteAsync(userId, cancellationToken);
        }

       public IAsyncEnumerable<int> ReturnValue(CancellationToken cancellationToken)
       {
            return serviceChannel.Reader.ReadAllAsync(cancellationToken);
        }
   }
}
