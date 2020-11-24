using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Application.Common.Marks;
using MediatR;

namespace Application.OrderApplication.CancelOrder
{
   public class CancelOrderCommand:IRequest<OperationResult<bool>>,ICommittable
    {
        public string OrderId { get; set; }
    }
}
