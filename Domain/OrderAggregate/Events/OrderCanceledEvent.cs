using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;
using DateTime = System.DateTime;

namespace Domain.OrderAggregate.Events
{
   public class OrderCanceledEvent:IDomainEvent
    {
        public Guid OrderId { get; set; }
        public DateTime CanceledDate { get; set; }

        public override string ToString()
        {
            return $"Order With Id {OrderId} Canceled At {CanceledDate}";
        }
    }
}
