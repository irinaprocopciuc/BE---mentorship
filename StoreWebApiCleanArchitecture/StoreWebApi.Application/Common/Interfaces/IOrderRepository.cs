using StoreWebAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreWebApi.Application.Common.Interfaces
{
    public interface IOrderRepository
    {
        public Task<List<Order>> GetOrders();
        public Task<Order> AddOrder(Order order);
    }
}
