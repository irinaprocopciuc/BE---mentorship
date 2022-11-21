using Store_Web_API.DataAccess;
using Store_Web_API.Filters.Custom_Exceptions;
using Store_Web_API.Models;
using Store_Web_API.Repositories;
using Store_Web_API.Repositories.Interfaces;
using System.Collections.Generic;

namespace Store_Web_API.Services
{
    public class ProductService: IProductService
    {
        private IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Product Add(Product product)
        {
            var existingProduct = _productRepository.GetById(product.ProductId);
            if (existingProduct != null)
            {
                throw new NotFoundDomainException("Cannot create product because already exists");
            }

            return _productRepository.Add(product);
        }

        public List<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public Product GetById(int productId)
        {
            var prod = _productRepository.GetById(productId);
            if (prod == null)
            {
                throw new NotFoundDomainException("Product id doesn't exist");
            }
            return prod;
        }

        public void Remove(int productId)
        {
            var product = GetById(productId);
            if(product == null)
            {
                throw new NotFoundDomainException("Product doesn't exist");
            }
            _productRepository.Remove(productId);
        }

        public void Update(Product product)
        {
            var existingProduct = GetById(product.ProductId);
            if(existingProduct == null)
            {
                throw new NotFoundDomainException("Product doesn't exist");
            }
            _productRepository.Update(product);
        }
    }
}
