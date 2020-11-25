using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Infrastructure.Repositories.EFCore.OrderRepositories.Contracts;
using MediatR;

namespace Application.OrderApplication.Commands.CancelOrder
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

            if(!order.CanBeCanceled())
                return OperationResult<bool>.FailureResult("You Cannot Cancel Order");

            order.CancelOrder();

            return OperationResult<bool>.SuccessResult(true);
        }
    }
}
