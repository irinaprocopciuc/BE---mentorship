using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store_Web_API.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CartId { get; set; }
        public Cart Cart { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public string PaymentMethod { get; set; }
    }
}
