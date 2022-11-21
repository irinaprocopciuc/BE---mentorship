using Store_Web_API.Models;
using System.Collections.Generic;

namespace Store_Web_API.Services
{
    public interface ICartService
    {
        void RemoveProductFromCart(int prodId, int cartId);
        Cart Add(Cart cart);
        Cart AddProductsInCart(List<CartProduct> products, int cartId);
        Cart GetCart(int cartId);
        CartProduct GetProductFromCart(int productId, int cartId);
    }
}