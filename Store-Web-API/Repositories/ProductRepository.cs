using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Store_Web_API.DataAccess;
using Store_Web_API.Models;
using Store_Web_API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store_Web_API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly WebStoreDBContext _context;

        public ProductRepository(WebStoreDBContext context)
        {
            _context = context;
        }

        public Product Add(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }

        public List<Product> GetAll()
        {
            return _context.Products.AsNoTracking().ToList();
        }

        public Product GetById(int productId)
        {
            return _context.Products.AsNoTracking().FirstOrDefault(prod => prod.ProductId == productId);
        }

        public void Remove(int productId)
        {
            var product = GetById(productId);
            _context.Products.Remove(product);
            _context.SaveChanges();
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }
    }
}
