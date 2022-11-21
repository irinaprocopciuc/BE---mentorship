using Microsoft.AspNetCore.Mvc;
using Store_Web_API.Models;
using Store_Web_API.Repositories.Interfaces;
using Store_Web_API.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Store_Web_API.Controllers
{
    [ApiController]
    [Route("store-api/orders")]
    public class OrderController: ControllerBase
    {
        private readonly IOrderService Orders;
        public OrderController(IOrderService orders)
        {
            Orders = orders;
        }


        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Orders.GetOrders());
        }


        [HttpPost]
        public IActionResult Post(Order order)
        {
            var orderResponse = Orders.AddOrder(order);
            var resourceUrl = Path.Combine(Request.Path.ToString(), Uri.EscapeDataString(orderResponse.OrderId.ToString())).Replace("\\", "/");
            return Created(resourceUrl, order);
        }
    }
}
