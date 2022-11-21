using Store_Web_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store_Web_API.Repositories.Interfaces
{
    public interface ICartRepository : IRepository
    {
        Cart AddProductsInCart(int cartId);
        Cart GetCart(int cartId);
        CartProduct GetProductFromCart(int productId, int cartId);
        void RemoveProductFromCart(CartProduct cartProduct);
        public List<CartProduct> GetCartProducts(int cartId);
        public void AddCartProduct(CartProduct prod);
    }
}
