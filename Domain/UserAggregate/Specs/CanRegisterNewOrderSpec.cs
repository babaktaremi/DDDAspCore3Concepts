using System;
using System.Linq;
using System.Linq.Expressions;
using Domain.Common;

namespace Domain.UserAggregate.Specs
{
   public class CanRegisterNewOrderSpec:Specification<User>
    {
        public override Expression<Func<User, bool>> ToExpression()
        {

            return user => user.Orders.OrderByDescending(c => c.Date.DateRegisterd).LastOrDefault().IsFinally || !user.Orders.Any();
        }
    }
}
