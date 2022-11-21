using StoreWebAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreWebApi.Application.Orders.Queries.GetOrders
{
    public class GetOrdersResponse
    {
        public GetOrdersResponse(IEnumerable<Order> orders)
        {
            Orders = orders;
        }

        public IEnumerable<Order> Orders { get; }
    }
}
