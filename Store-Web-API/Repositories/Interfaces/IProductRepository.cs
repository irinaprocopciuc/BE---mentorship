using Store_Web_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store_Web_API.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Product Add(Product product);
        List<Product> GetAll();
        Product GetById(int productId);
        void Remove(int productId);
        void Update(Product product);

    }
}
