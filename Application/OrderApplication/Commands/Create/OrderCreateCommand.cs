using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Application.Common.Marks;
using MediatR;

namespace Application.OrderApplication.Commands.Create
{
   public class OrderCreateCommand:IRequest<OperationResult<OrderCreateCommandResult>>,ICommittable
    {
        public int UserId { get; set; }
        public int NumberOfItems { get; set; }
        public int TotalPrice { get; set; }

    }
}
