using StoreWebApi.Domain.Entities;

namespace StoreWebAPI.Domain.Entities
{
    public class Order: BaseEntity
    {
        public Order(int orderId, int cartId, Cart cart, string name, string adress, string phone, string paymentMethod): base(orderId)
        {
            CartId = cartId;
            Cart = cart;
            Name = name;
            Adress = adress;
            Phone = phone;
            PaymentMethod = paymentMethod;
        }

        protected Order() { }

        public int CartId { get;  set; }
        public Cart Cart { get;  set; }
        public string Name { get;  set; }
        public string Adress { get;  set; }
        public string Phone { get;  set; }
        public string PaymentMethod { get; set; }
    }
}
