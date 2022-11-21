using MediatR;
using StoreWebApi.Application.Carts.Queries.GetCart;
using StoreWebApi.Application.Common.Interfaces;
using StoreWebApi.Application.Orders.Queries.GetOrders;
using StoreWebApi.Application.Specifications;
using StoreWebAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StoreWebApi.Application.Orders.Queries
{
    public class GetOrdersHandler: IRequestHandler<GetOrdersQuery, GetOrdersResponse>
    {
        private readonly IStoreRepository _storeRepository;

        public GetOrdersHandler(IStoreRepository storeRepository) => _storeRepository = storeRepository;

        public async Task<GetOrdersResponse> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var ordersSpec = new GetAllSpecification<Order>();
            var orders = await _storeRepository.ExecuteQuery(ordersSpec);

            foreach (Order order in orders)
            {
                var cartSpec = new GetCartSpecification(order.CartId);
                order.Cart = await _storeRepository.FirstOrDefault(cartSpec);
            }

            var response = new GetOrdersResponse(orders);
            return response;
        }
    }
}
