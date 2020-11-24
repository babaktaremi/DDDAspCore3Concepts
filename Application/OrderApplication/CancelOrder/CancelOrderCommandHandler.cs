using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Infrastructure.Repositories.EFCore.OrderRepositories.Contracts;
using MediatR;

namespace Application.OrderApplication.CancelOrder
{
   public class CancelOrderCommandHandler:IRequestHandler<CancelOrderCommand,OperationResult<bool>>
   {
       private readonly IOrderRepository _orderRepository;

       public CancelOrderCommandHandler(IOrderRepository orderRepository)
       {
           _orderRepository = orderRepository;
       }

        public async Task<OperationResult<bool>> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            var order =await _orderRepository.GetOrder(Guid.Parse(request.OrderId));

            if(order is null)
                return OperationResult<bool>.FailureResult("Order Not Found");

            order.CancelOrder();

            return OperationResult<bool>.SuccessResult(true);
        }
    }
}
