using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreWebApi.Application.Common.Interfaces;
using StoreWebApi.Application.Products.Commands.AddProduct;
using StoreWebApi.Application.Products.Commands.RemoveProduct;
using StoreWebApi.Application.Products.Commands.UpdateProducts;
using StoreWebApi.Application.Products.Queries;
using StoreWebAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StoreWebApi.API.Controllers
{
    [ApiController]
    [Route("store-api/products")]
    public class ProductsController : Controller
    {

        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<Product>> Get()
        {
            return (await _mediator.Send(new GetAllProductsQuery())).Products;
        }

        [HttpGet]
        [Route("{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<Product> Get(int productId)
        {
            return (await _mediator.Send(new GetByIdQuery(productId))).Product;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Post([FromBody] ProductDTO prod)
        {
            await _mediator.Send(new AddProductCommand(prod));
            var resourceUrl = Path.Combine(Request.Path.ToString(), Uri.EscapeDataString(prod.Name.ToString())).Replace("\\", "/");
            return Created(resourceUrl, prod);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(Product prod)
        {
            await _mediator.Send(new UpdateProductCommand(prod));
            var resourceUrl = Path.Combine(Request.Path.ToString(), Uri.EscapeDataString(prod.Id.ToString())).Replace("\\", "/");
            return Created(resourceUrl, prod);
        }

        [HttpDelete]
        [Route("{productId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int productId)
        {
            await _mediator.Send(new RemoveProductCommand(productId));
            return NoContent();
        }

    }
}
