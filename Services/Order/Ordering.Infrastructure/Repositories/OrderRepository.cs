using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Entities;
using Ordering.Domain.Repositories;
using Ordering.Infrastructure.Data;
using Ordering.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(OrderContext orderContext) :base(orderContext) 
        { 
        
        }
    

        public async Task<IEnumerable<Order>> GetOrderBySellerUserName(string userName)
        {
            var orderList = await _dbContext.Orders.Where(x => x.SellerUserName.Equals(userName)).ToListAsync();
            return orderList;
        }
    }
}
