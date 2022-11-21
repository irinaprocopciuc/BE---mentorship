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
    [Route("store-api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService products;

        public ProductController(IProductService products)
        {
            this.products = products;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(products.GetAll());
        }

        [HttpGet]
        [Route("{productId}")]
        public IActionResult Get(int productId)
        {
            return Ok(products.GetById(productId));
        }

        [HttpPost]
        public IActionResult Post(Product product)
        {
            var prod = products.Add(product);
            var resourceUrl = Path.Combine(Request.Path.ToString(), Uri.EscapeDataString(prod.ProductId.ToString())).Replace("\\", "/");
            return Created(resourceUrl, product);
        }

        [HttpPut]
        public IActionResult Put(Product product)
        {
           products.Update(product);
           return NoContent();
        }

        [HttpDelete]
        [Route("{productId}")]
        public IActionResult Delete(int productId)
        {
            products.Remove(productId);
            return NoContent();
        }
    }
}
