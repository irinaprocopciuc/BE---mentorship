using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreWebApi.Application.Orders.Commands.AddOrder;
using StoreWebApi.Application.Orders.Queries.GetOrders;
using StoreWebAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace StoreWebApi.API.Controllers
{
    [ApiController]
    [Route("store-api/orders")]
    public class OrdersController : Controller
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<Order>> Get()
        {
            return (await _mediator.Send(new GetOrdersQuery())).Orders;
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] OrderDTO order)
        {
            await _mediator.Send(new AddOrderCommand(order));
            var resourceUrl = Path.Combine(Request.Path.ToString(), Uri.EscapeDataString(order.Name.ToString())).Replace("\\", "/");
            return Created(resourceUrl, order);
        }
    }
}
