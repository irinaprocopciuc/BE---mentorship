using StoreWebAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreWebApi.Application.Common.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetById(int productId);
        Task<List<Product>> GetAll();
        Task<Product> Add(Product product);
        Task<int> Remove(int productId);
        Task<Product> Update(Product product);
    }
}
