namespace StoreWebApi.Application.Carts.Commands.AddProductInCart
{
    public class CartProductDTO
    {
        public CartProductDTO(int productid, int quantity)
        {
            ProductId = productid;
            Quantity = quantity;
        }

        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
