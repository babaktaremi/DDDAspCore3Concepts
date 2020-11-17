using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.OrderAggregate.ValueObjects
{
   public record OrderDetail(int UserId,int TotalPrice,int NumberOfItems);
}
