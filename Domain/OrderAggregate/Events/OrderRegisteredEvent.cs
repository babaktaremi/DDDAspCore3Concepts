using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.OrderAggregate.Events
{
   public class OrderRegisteredEvent:IDomainEvent
    {
        public int UserId { get; set; }
        public DateTime DateTime { get; set; }
    }
}
