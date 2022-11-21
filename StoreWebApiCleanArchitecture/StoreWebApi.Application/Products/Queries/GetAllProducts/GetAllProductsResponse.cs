using StoreWebAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreWebApi.Application.Products.Queries.GetAllProducts
{
    public class GetAllProductsResponse
    {

        public GetAllProductsResponse(IEnumerable<Product> products)
        {
            Products = products;
        }

        public IEnumerable<Product> Products { get; }
    }
}
