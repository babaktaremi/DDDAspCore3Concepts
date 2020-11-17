using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.OrderAggregate.Enums
{
   public class OrderState:Enumeration
    {
        public OrderState(int id, string name) : base(id, name)
        {
        }

        public static OrderState New=new OrderState(1,"New");
        public static OrderState Paid=new OrderState(2,"Paid");
        public static OrderState Cancelled=new OrderState(3,"Cancelled");

       
    }
}
