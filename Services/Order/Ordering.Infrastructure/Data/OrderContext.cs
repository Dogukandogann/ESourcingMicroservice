using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Data
{
    public class OrderContext :DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {
            
        }
        public DbSet<Order> Orders { get; set; }
        
    }
}
