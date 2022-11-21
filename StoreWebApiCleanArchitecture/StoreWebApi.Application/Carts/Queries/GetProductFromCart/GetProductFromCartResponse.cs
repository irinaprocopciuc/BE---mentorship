using StoreWebAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreWebApi.Application.Carts.Queries.GetProductFromCart
{
    public class GetProductFromCartResponse
    {
        public GetProductFromCartResponse(CartProduct product)
        {
            CartProduct = product;
        }

        public CartProduct CartProduct { get; }
    }
}
