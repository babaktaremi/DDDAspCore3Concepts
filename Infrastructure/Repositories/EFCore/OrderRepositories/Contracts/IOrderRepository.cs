using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.OrderAggregate;

namespace Infrastructure.Repositories.EFCore.OrderRepositories.Contracts
{
   public interface IOrderRepository
   {
       void CreateOrder(Order order);
   }
}
