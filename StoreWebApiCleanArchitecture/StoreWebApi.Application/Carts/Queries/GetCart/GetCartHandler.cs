using MediatR;
using StoreWebApi.Application.Carts.Queries.GetCart;
using StoreWebApi.Application.Common.Interfaces;
using StoreWebAPI.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace StoreWebApi.Application.Carts.Queries
{
    public class GetCartHandler: IRequestHandler<GetCartQuery, GetCartResponse>
    {

        private readonly IStoreRepository _storeRepository;

        public GetCartHandler(IStoreRepository storeRepository) => _storeRepository = storeRepository;

        public async Task<GetCartResponse> Handle(GetCartQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetCartSpecification(request.cartId);
            var cart = await _storeRepository.FirstOrDefault(specification);

            var response = new GetCartResponse(cart);
            return response;
        }
    }
}
