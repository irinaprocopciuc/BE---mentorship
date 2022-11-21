using Microsoft.EntityFrameworkCore;
using Store_Web_API.DataAccess;
using Store_Web_API.Models;
using Store_Web_API.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Store_Web_API.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly WebStoreDBContext _context;

        public OrderRepository(WebStoreDBContext context)
        {
            _context = context;
        }

        public List<Order> GetOrders()
        {
            return _context.Orders.AsNoTracking().ToList();
        }

        public Order AddOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
            return order;
        }
    }
}
