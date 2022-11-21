using MediatR;
using StoreWebApi.Application.Common.Exceptions;
using StoreWebApi.Application.Common.Interfaces;
using StoreWebApi.Application.Products.Commands.RemoveProduct;
using StoreWebApi.Application.Specifications;
using StoreWebAPI.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace StoreWebApi.Application.Products.Commands
{
    public class RemoveProductHandler: IRequestHandler<RemoveProductCommand, Unit>
    {
        private readonly IStoreRepository _storeRepository;

        public RemoveProductHandler(IStoreRepository storeRepository) => _storeRepository = storeRepository;

        public async Task<Unit> Handle(RemoveProductCommand request, CancellationToken cancellationToken)
        {
            var specification = new GetByIdSpecification<Product>(request.productId);
            var product = await _storeRepository.FirstOrDefault(specification);
            if (product == null)
            {
                throw new NotFoundDomainException("Product doesn't exist");
            }

            await _storeRepository.RemoveAsync<Product>(request.productId);

            return Unit.Value;
        }
    }
}
