using StoreWebAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreWebApi.Application.Common.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart> AddCart(Cart cart);
        Task<Cart> AddProductsInCart(List<CartProduct> products, int cartId);
        Task<Cart> GetCart(int cartId);
        Task<CartProduct> GetProductFromCart(int productId, int cartId);
        Task<List<CartProduct>> GetCartProducts(int cartId);
        Task RemoveProductFromCart(int prodId, int cartId);
        Task AddCartProduct(CartProduct prod);
    }
}
