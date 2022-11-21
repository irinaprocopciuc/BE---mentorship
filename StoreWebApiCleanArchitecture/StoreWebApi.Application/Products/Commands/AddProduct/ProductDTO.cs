using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreWebApi.Application.Products.Commands.AddProduct
{
    public class ProductDTO
    {

        public ProductDTO(string name, string description, int stock, int price, string category) 
        {
            Name = name;
            Description = description;
            Stock = stock;
            Price = price;
            Category = category;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public int Price { get; set; }
        public string Category { get; set; }
    }
}
