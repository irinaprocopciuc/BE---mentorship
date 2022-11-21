using MediatR;
using StoreWebApi.Application.Carts.Queries.GetProductFromCart;
using StoreWebApi.Application.Common.Interfaces;
using StoreWebAPI.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace StoreWebApi.Application.Carts.Queries
{
    public class GetProductFromCartHandler : IRequestHandler<GetProductFromCartQuery, GetProductFromCartResponse>
    {

        private readonly IStoreRepository _storeRepository;

        public GetProductFromCartHandler(IStoreRepository storeRepository) => _storeRepository = storeRepository;

        public async Task<GetProductFromCartResponse> Handle(GetProductFromCartQuery request, CancellationToken cancellationToken)
        {
            var spec = new GetCartProdByCartIdAndProdIdSpecification(request.cartId, request.productId);
            var cartProduct = await _storeRepository.FirstOrDefault(spec);

            var response = new GetProductFromCartResponse(cartProduct);
            return response;
        }
    }
}
