using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreWebApi.Application.Carts.Commands;
using StoreWebApi.Application.Carts.Commands.AddProductInCart;
using StoreWebApi.Application.Carts.Commands.RemoveProductsFromCart;
using StoreWebApi.Application.Carts.Queries.GetCart;
using StoreWebApi.Domain.Entities;
using StoreWebAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StoreWebApi.API.Controllers
{
    [ApiController]
    [Route("store-api/carts")]
    public class CartsController : ControllerBase
    {

        private readonly IMediator _mediator;

        public CartsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("{cartId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int cartId)
        {
            return Ok((await _mediator.Send(new GetCartQuery(cartId))).Cart);
        }


        [HttpPost("")]
        public async Task<IActionResult> AddCart([FromBody] Cart cart)
        {
            await _mediator.Send(new AddCartCommand(cart));
            var resourceUrl = Path.Combine(Request.Path.ToString(), Uri.EscapeDataString(cart.Id.ToString())).Replace("\\", "/");
            return Created(resourceUrl, cart);
        }

        [HttpPost("{cartId}/products")]
        public async Task<IActionResult> AddproductsInCart([FromBody] List<CartProductDTO> products, int cartId)
        {
            await _mediator.Send(new AddProductInCartCommand(products, cartId));
            var resourceUrl = Path.Combine(Request.Path.ToString(), Uri.EscapeDataString(cartId.ToString())).Replace("\\", "/");
            return Created(resourceUrl, products);
        }

        [HttpDelete]
        [Route("{productId}/{cartId}")]
        public async Task<IActionResult> Delete(int productId, int cartId)
        {
            await _mediator.Send(new RemoveProductsFromCartCommand(productId, cartId));
            return NoContent();
        }
    }
}
