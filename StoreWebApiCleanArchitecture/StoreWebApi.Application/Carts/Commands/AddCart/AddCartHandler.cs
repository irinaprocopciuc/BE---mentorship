using MediatR;
using StoreWebApi.Application.Common.Exceptions;
using StoreWebApi.Application.Common.Interfaces;
using StoreWebApi.Application.Specifications;
using StoreWebAPI.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace StoreWebApi.Application.Carts.Commands.AddCart
{
    public class AddCartHandler: IRequestHandler<AddCartCommand, Unit>
    {

        private readonly IStoreRepository _storeRepository;

        public AddCartHandler(IStoreRepository soreRepository) => _storeRepository = soreRepository;

        public async Task<Unit> Handle(AddCartCommand request, CancellationToken cancellationToken)
        {
            var spec = new GetByIdSpecification<Cart>(request.Cart.Id);
            var result = await _storeRepository.FirstOrDefault(spec);
            if (result != null)
            {
                throw new EntityAlreadyExistsException("Cart already exists");
            }

            Cart cart = new Cart();

            await _storeRepository.AddAsync(cart);

            return Unit.Value;
        }
    }
}
