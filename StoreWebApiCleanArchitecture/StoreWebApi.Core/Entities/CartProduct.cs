using StoreWebApi.Domain.Entities;

namespace StoreWebAPI.Domain.Entities
{
    public class CartProduct: BaseEntity
    {
        public CartProduct(int id, int productId, Product product, string name, int price, int quantity, int cartId): base(id)
        {
            ProductId = productId;
            Product = product;
            Name = name;
            Price = price;
            Quantity = quantity;
            CartId = cartId;
        }

        protected CartProduct() { }

        public void ChangeQuantity(int newQuantity)
        {
            Quantity = newQuantity;
        }

        public int ProductId { get; private set; }
        public virtual Product Product { get; private set; }
        public string Name { get; private set; }
        public int Price { get; private set;  }
        public int Quantity { get; private set; }

        public int CartId { get; private set;}
    }
}
