using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreWebApi.Application.Carts.Queries.GetProductFromCart
{
    public record GetProductFromCartQuery(int productId, int cartId): IRequest<GetProductFromCartResponse>
    {
    }
}
