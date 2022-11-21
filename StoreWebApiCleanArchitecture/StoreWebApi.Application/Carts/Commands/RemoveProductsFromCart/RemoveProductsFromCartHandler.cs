using MediatR;
using StoreWebApi.Application.Carts.Commands.RemoveProductsFromCart;
using StoreWebApi.Application.Common.Exceptions;
using StoreWebApi.Application.Common.Interfaces;
using StoreWebApi.Application.Specifications;
using StoreWebAPI.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace StoreWebApi.Application.Carts.Commands
{
    public class RemoveProductsFromCartHandler: IRequestHandler<RemoveProductsFromCartCommand, Unit>
    {
        private readonly IStoreRepository _storeRepository;

        public RemoveProductsFromCartHandler(IStoreRepository storeRepository) => _storeRepository = storeRepository;

        public async Task<Unit> Handle(RemoveProductsFromCartCommand request, CancellationToken cancellationToken)
        {
            var cartSpec = new GetByIdSpecification<Cart>(request.cartId);
            var cart = await _storeRepository.FirstOrDefault(cartSpec);
            if (cart == null)
            {
                throw new NotFoundDomainException("Cart doesn't exist");
            }
            var cartProdSpec = new GetCartProductsByCartIdSpecification(cart.Id);
            var existingProduct = await _storeRepository.FirstOrDefault(cartProdSpec);
            if (existingProduct == null)
            {
                throw new NotFoundDomainException("Already deleted");
            }

            await _storeRepository.RemoveAsync<CartProduct>(request.productId);

            return Unit.Value;
        }
    }
}
