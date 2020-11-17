using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;
using Domain.UserAggregate;

namespace Domain.OrderAggregate.Specs
{
   public class CanRegisterNewOrderSpec:Specification<User>
    {
        public override Expression<Func<User, bool>> ToExpression()
        {
            return user => user.Orders.OrderByDescending(c => c.Date.DateRegisterd).LastOrDefault().IsFinally;
        }
    }
}
