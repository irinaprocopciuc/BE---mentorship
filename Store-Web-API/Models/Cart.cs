using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store_Web_API.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        public List<CartProduct> Products { get; set; }
        public int Discount { get; set; }
        public decimal Total {get; set;}

    }
}
