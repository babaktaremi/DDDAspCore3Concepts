using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.UserAggregate.Events
{
   public class UserLoginEvent:IDomainEvent
    {
        public int UserId { get; set; }
    }
}
