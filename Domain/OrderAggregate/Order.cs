using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;
using Domain.OrderAggregate.Enums;
using Domain.OrderAggregate.Events;
using Domain.OrderAggregate.Specs;
using Domain.OrderAggregate.ValueObjects;
using Domain.UserAggregate;

namespace Domain.OrderAggregate
{
   public class Order:BaseEntity<Guid>
    {
        public Order()
        {
            OrderState=OrderState.New;
        }


        public OrderDate Date { get; set; }
        public OrderDetail Detail { get; set; }
        public bool IsFinally { get; set; }
        public OrderState OrderState { get; set; }

        #region Navigation Proporties

        public int UserId { get; set; }
        public User User { get; set; }
        #endregion


        #region Methods

        public bool CanBeCanceled()
        {
            var cancelSpec = new OrderCancellationSpec();

            return cancelSpec.IsSatisfiedBy(this);
        }

        public void CancelOrder()
        {
            this.OrderState=OrderState.Cancelled;
            RaiseDomainEvent(new OrderCanceledEvent{OrderId = this.Id,CanceledDate =DateTime.Now });
        }


        #endregion
    }
}
