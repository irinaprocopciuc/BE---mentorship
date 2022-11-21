using Store_Web_API.Models;
using System.Collections.Generic;

namespace Store_Web_API.Services
{
    public interface IProductService
    {
        public Product Add(Product product);
        public List<Product> GetAll();

        public Product GetById(int productId);

        public void Remove(int productId);

        public void Update(Product product);
    }
}