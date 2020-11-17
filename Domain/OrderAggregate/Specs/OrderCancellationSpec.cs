﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.OrderAggregate.Specs
{
   public class OrderCancellationSpec:Specification<Order>
    {
        public override Expression<Func<Order, bool>> ToExpression()
        {
            return order => order.IsFinally == false && order.Date.DateRegisterd < DateTime.Now.AddMinutes(-20);
        }
    }
}