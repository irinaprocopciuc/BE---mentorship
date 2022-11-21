using MediatR;
using StoreWebApi.Application.Common.Exceptions;
using StoreWebApi.Application.Common.Interfaces;
using StoreWebApi.Application.Products.Commands.AddProduct;
using StoreWebApi.Application.Specifications;
using StoreWebAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StoreWebApi.Application.Products.Commands
{
    public class AddProductHandler: IRequestHandler<AddProductCommand, Unit>
    {
        private readonly IStoreRepository _storeRepository;

        public AddProductHandler(IStoreRepository storeRepository) => _storeRepository = storeRepository;

        public async Task<Unit> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            //var specification = new GetByIdSpecification<Product>(request.Product.Id);
            //var existingProduct = await _storeRepository.FirstOrDefault(specification);
            //if (existingProduct != null)
            //{
            //    throw new NotFoundDomainException("Cannot create product because already exists");
            //}

            var productToAdd = new Product(0, request.Product.Name, request.Product.Description, request.Product.Stock, request.Product.Price, request.Product.Category);
            await _storeRepository.AddAsync(productToAdd);

            return Unit.Value;
        }
    }
}
