using MediatR;
using StoreWebApi.Application.Common.Exceptions;
using StoreWebApi.Application.Common.Interfaces;
using StoreWebApi.Application.Specifications;
using StoreWebAPI.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace StoreWebApi.Application.Products.Queries.GetById
{
    public class GetByIdHandler: IRequestHandler<GetByIdQuery, GetByIdResponse>
    {

        private readonly IStoreRepository _storeRepository;

        public GetByIdHandler(IStoreRepository storeRepository) => _storeRepository = storeRepository;

        public async Task<GetByIdResponse> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetByIdSpecification<Product>(request.productId);
            var product = await _storeRepository.FirstOrDefault(specification);
            if (product == null)
            {
                throw new NotFoundDomainException("Product id doesn't exist");
            }

            var response = new GetByIdResponse(product);
            return response;
        }
    }
}
