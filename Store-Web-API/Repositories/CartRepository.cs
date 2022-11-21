using Microsoft.EntityFrameworkCore;
using Store_Web_API.DataAccess;
using Store_Web_API.Models;
using Store_Web_API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Store_Web_API.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly WebStoreDBContext _context;

        public CartRepository(WebStoreDBContext context)
        {
            _context = context;
        }


        public T Add<T>(T cart) where T:class
        {
            _context.Set<T>().Add(cart);
            _context.SaveChanges();
            return cart;

            //var necart = (object)cart;
            //_context.Carts.Add((Cart)necart);
            //_context.SaveChanges();
        }

        public Cart AddProductsInCart(int cartId)
        {
            _context.SaveChanges();

            return GetCart(cartId);
        }

        public Cart GetCart(int cartId)
        {
            var cart = _context.Carts.AsNoTracking().FirstOrDefault(c => c.CartId.Equals(cartId));
            if(cart != null)
            {
                cart.Products = _context.CartProducts.AsNoTracking().Where(r => r.CartId.Equals(cart.CartId)).ToList();
            }
            return cart;
        }


        public CartProduct GetProductFromCart(int productId, int cartId)
        {
            return _context.CartProducts.AsNoTracking().FirstOrDefault(e => e.ProductId == productId && e.CartId == cartId);
        }

        public void RemoveProductFromCart(CartProduct cartProduct)
        {
           _context.CartProducts.Remove(cartProduct);
           _context.SaveChanges();
        }

        public List<CartProduct> GetCartProducts(int cartId)
        {
            return _context.CartProducts.AsNoTracking().Where(r => r.CartId.Equals(cartId)).ToList();
        }

        public void AddCartProduct(CartProduct prod)
        {
            _context.CartProducts.Add(prod);
            _context.SaveChanges();
        }
    }
}
