using StoreWebApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreWebAPI.Domain.Entities
{
    public class Product: BaseEntity
    {
        public Product(int id, string name, string description, int stock, int price, string category): base(id)
        {
            Name = name;
            Description = description;
            Stock = stock;
            Price = price;
            Category = category;
        }

        protected Product() { }

        public string Name { get; set; }
        public string Description { get;  set; }
        public int Stock { get;  set; }
        public int Price { get;  set; }
        public string Category { get;  set; }
    }
}
