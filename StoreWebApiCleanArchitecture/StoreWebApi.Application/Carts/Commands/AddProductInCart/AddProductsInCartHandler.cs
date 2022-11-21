using MediatR;
using StoreWebApi.Application.Carts.Commands.AddProductInCart;
using StoreWebApi.Application.Carts.Queries.GetCart;
using StoreWebApi.Application.Common.Exceptions;
using StoreWebApi.Application.Common.Interfaces;
using StoreWebApi.Application.Specifications;
using StoreWebApi.Domain.Entities;
using StoreWebAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StoreWebApi.Application.Carts.Commands
{
    public class AddProductsInCartHandler: IRequestHandler<AddProductInCartCommand , Unit>
    {
        private readonly IStoreRepository _storeRepository;

        public AddProductsInCartHandler(IStoreRepository storeRepository) => _storeRepository = storeRepository;

        public async Task<Unit> Handle(AddProductInCartCommand request, CancellationToken cancellationToken)
        {
            var spec = new GetCartSpecification(request.cartId);
            var cart = await _storeRepository.FirstOrDefault(spec, false);
            if (cart == null)
            {
                throw new NotFoundDomainException("Cart doesn't exist");
            }

            var productSpec = new GetProductsByIdsSpec(request.Products.Select(p => p.ProductId).ToList());
            var productDetails = await _storeRepository.ExecuteQuery(productSpec);

            var cartProductsList = productDetails.Select(p => 
                    new CartProduct(0, p.Id, p, p.Name, p.Price, request.Products.First(cp => cp.ProductId == p.Id).Quantity, request.cartId)).ToList();


            cart.AddProductsInCart(cartProductsList);
            await _storeRepository.UpdateAsync(cart);

            return Unit.Value;
        }
    }
}
