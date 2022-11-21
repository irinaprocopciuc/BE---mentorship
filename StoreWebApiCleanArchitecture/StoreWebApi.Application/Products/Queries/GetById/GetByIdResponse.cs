using StoreWebAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreWebApi.Application.Products.Queries.GetById
{
    public class GetByIdResponse
    {
        public GetByIdResponse(Product product)
        {
            Product = product;
        }

        public Product Product { get; }
    }
}
