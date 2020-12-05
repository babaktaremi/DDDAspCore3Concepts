using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Domain.OrderAggregate;
using Domain.ValueObjects;
using Infrastructure.Repositories.EFCore.OrderRepositories.Contracts;
using Infrastructure.Repositories.EFCore.UserRepositories.Contracts;
using MediatR;

namespace Application.OrderApplication.Commands.Create
{
   public class OrderCreateCommandHandler:IRequestHandler<OrderCreateCommand,OperationResult<OrderCreateCommandResult>>
   {
       private readonly IOrderRepository _orderRepository;
       private readonly IUserRepository _userRepository;

       public OrderCreateCommandHandler(IOrderRepository orderRepository, IUserRepository userRepository)
       {
           _orderRepository = orderRepository;
           _userRepository = userRepository;
       }

       public async Task<OperationResult<OrderCreateCommandResult>> Handle(OrderCreateCommand request, CancellationToken cancellationToken)
       {
           var user = await _userRepository.GetUserWithOrders(request.UserId);

           if (user.Orders.Any())
           {
               if (!user.CanRegisterOrder())
                   return OperationResult<OrderCreateCommandResult>.FailureResult("You have Unfinished Order");
            }

           var order = new Order
           {
               Date = new OrderDate(DateTime.Now),
               Detail = new OrderDetail(request.UserId, request.TotalPrice, request.NumberOfItems),
               UserId = request.UserId
           };

           _orderRepository.CreateOrder(order);

           order.RegisterOrder(request.UserId,DateTime.Now);

           return OperationResult<OrderCreateCommandResult>.SuccessResult(new OrderCreateCommandResult
               {RegisteredDate = DateTime.Now, State = order.OrderState.Name,OrderId = order.Id});
       }
    }
}
