using Store_Web_API.DataAccess;
using Store_Web_API.Models;
using Store_Web_API.Repositories;
using Store_Web_API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store_Web_API.Services
{
    public class OrderService: IOrderService
    {
        private IOrderRepository _orderRepository;
        private ICartRepository _cartRepository;

        public OrderService(IOrderRepository orderRepository, ICartRepository cartRepository)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
        }

        public List<Order> GetOrders()
        {
            var orders = _orderRepository.GetOrders();
            foreach (Order order in orders)
            {
                order.Cart = _cartRepository.GetCart(order.CartId);
                order.Cart.Products = _cartRepository.GetCartProducts(order.CartId);
            }
            return orders;
        }

        public Order AddOrder(Order order)
        {
            return _orderRepository.AddOrder(order);
        }
    }
}
