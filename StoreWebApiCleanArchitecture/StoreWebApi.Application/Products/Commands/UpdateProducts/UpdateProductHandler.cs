using MediatR;
using StoreWebApi.Application.Common.Exceptions;
using StoreWebApi.Application.Common.Interfaces;
using StoreWebApi.Application.Products.Commands.AddProduct;
using StoreWebApi.Application.Products.Commands.UpdateProducts;
using StoreWebApi.Application.Specifications;
using StoreWebAPI.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace StoreWebApi.Application.Products.Commands
{
    public class UpdateProductHandler: IRequestHandler<UpdateProductCommand, Unit>
    {
        private readonly IStoreRepository _storeRepository;

        public UpdateProductHandler(IStoreRepository storeRepository) => _storeRepository = storeRepository;

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var specification = new GetByIdSpecification<Product>(request.product.Id);
            var existingProduct = await _storeRepository.FirstOrDefault(specification);
            if (existingProduct == null)
            {
                throw new NotFoundDomainException("Product doesn't exist");
            }

            await _storeRepository.UpdateAsync(request.product);

            return Unit.Value;
        }
    }
}
