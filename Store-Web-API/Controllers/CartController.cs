using Microsoft.AspNetCore.Mvc;
using Store_Web_API.Models;
using Store_Web_API.Repositories.Interfaces;
using Store_Web_API.Services;
using System;
using System.Collections.Generic;
using System.IO;

namespace Store_Web_API.Controllers
{
    [ApiController]
    [Route("store-api/carts")]
    public class CartController: ControllerBase
    {
        private readonly ICartService CartService;
        public CartController(ICartService cartService)
        {
            CartService = cartService;
        }


        [HttpGet]
        [Route("{cartId}")]
        public IActionResult Get(int cartId)
        {
            return Ok(CartService.GetCart(cartId));
        }

        [HttpPost("{cartId}/products")]
        public IActionResult AddProductsInCart(List<CartProduct> products, int cartId)
        {
            var cart = CartService.AddProductsInCart(products, cartId);
            var resourceUrl = Path.Combine(Request.Path.ToString(), Uri.EscapeDataString(cart.CartId.ToString())).Replace("\\", "/");
            return Created(resourceUrl, cart);
        }


        [HttpPost("cart")]
        public IActionResult AddCart(Cart cart)
        {
            var cartResponse = CartService.Add(cart);
            var resourceUrl = Path.Combine(Request.Path.ToString(), Uri.EscapeDataString(cartResponse.CartId.ToString())).Replace("\\", "/");
            return Created(resourceUrl, cart);
        }


        [HttpDelete]
        [Route("{productId}/{cartId}")]
        public IActionResult Delete(int productId, int cartId)
        {
            CartService.RemoveProductFromCart(productId, cartId);
            return NoContent();
        }
    }
}
