using Store_Web_API.Models;
using System.Collections.Generic;

namespace Store_Web_API.Services
{
    public interface IOrderService
    {
        public List<Order> GetOrders();

        public Order AddOrder(Order order);
    }
}