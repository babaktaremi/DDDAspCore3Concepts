using Application.Common;
using Application.Common.Marks;
using MediatR;

namespace Application.OrderApplication.Commands.CancelOrder
{
   public class CancelOrderCommand:IRequest<OperationResult<bool>>,ICommittable
    {
        public string OrderId { get; set; }
    }
}
