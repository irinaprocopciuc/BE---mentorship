using MediatR;
using StoreWebApi.Application.Common.Interfaces;
using StoreWebApi.Application.Specifications;
using StoreWebAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StoreWebApi.Application.Products.Queries.GetAllProducts
{
    public class GetAllProductsHandler: IRequestHandler<GetAllProductsQuery, GetAllProductsResponse>
    {
        private readonly IStoreRepository _storeRepository;

        public GetAllProductsHandler(IStoreRepository storeRepository) => _storeRepository = storeRepository;

        public async Task<GetAllProductsResponse> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetAllSpecification<Product>();
            IEnumerable<Product> products = await _storeRepository.ExecuteQuery(specification);
            if(products == null)
            {
                return null;
            }

            var response = new GetAllProductsResponse(products);
            return response;
        }
    }
}
