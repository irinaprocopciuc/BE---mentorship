using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreWebApi.Application.Orders.Commands.AddOrder
{
    public class OrderDTO
    {

        public OrderDTO( int cartId,  string name, string adress, string phone, string paymentMethod)
        {
            CartId = cartId;
            Name = name;
            Adress = adress;
            Phone = phone;
            PaymentMethod = paymentMethod;
        }

        public int CartId { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public string PaymentMethod { get; set; }
    }
}
