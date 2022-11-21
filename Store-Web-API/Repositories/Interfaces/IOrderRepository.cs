using Store_Web_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store_Web_API.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        public List<Order> GetOrders();
        public Order AddOrder(Order order);
    }
}
