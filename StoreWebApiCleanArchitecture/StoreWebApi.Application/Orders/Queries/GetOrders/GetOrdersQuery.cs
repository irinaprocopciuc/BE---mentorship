using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreWebApi.Application.Orders.Queries.GetOrders
{
    public record GetOrdersQuery: IRequest<GetOrdersResponse>
    {
    }
}
