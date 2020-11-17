using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.OrderAggregate;
using Infrastructure.Persistence;
using Infrastructure.Repositories.EFCore.Common.BaseRepository;
using Infrastructure.Repositories.EFCore.OrderRepositories.Contracts;
using Utility;

namespace Infrastructure.Repositories.EFCore.OrderRepositories.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository,IScopedDependency
    {
        public OrderRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public void CreateOrder(Order order)
        {
           base.Add(order);
        }

    }
}
