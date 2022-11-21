using StoreWebAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreWebApi.Application.Carts.Queries.GetCart
{
    public class GetCartResponse
    {
        public GetCartResponse(Cart cartDetails)
        {
            Cart = cartDetails;
        }

        public Cart Cart { get; }
    }
}
