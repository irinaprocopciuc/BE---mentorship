using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreWebApi.Application.Carts.Queries.GetCart
{
    public record GetCartQuery(int cartId) : IRequest<GetCartResponse>
    {
    }
}
