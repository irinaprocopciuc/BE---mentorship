using Store_Web_API.DataAccess;
using Store_Web_API.Filters.Custom_Exceptions;
using Store_Web_API.Models;
using Store_Web_API.Repositories;
using Store_Web_API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Store_Web_API.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public void RemoveProductFromCart(int prodId, int cartId)
        {
            var existingProduct = _cartRepository.GetProductFromCart(prodId, cartId);
            if (existingProduct == null)
            {
                throw new NotFoundDomainException("Already deleted");
            }
            var cart = _cartRepository.GetCart(cartId);
            if (cart == null)
            {
                throw new NotFoundDomainException("Cart doesn't exist");
            }
            _cartRepository.RemoveProductFromCart(existingProduct);
        }


        public Cart Add(Cart cart)
        {
            return _cartRepository.Add(cart);
        }

        public Cart AddProductsInCart(List<CartProduct> products, int cartId)
        {
            Random rnd = new Random();
            var cart = _cartRepository.GetCart(cartId);
            if (cart == null)
            {
                throw new NotFoundDomainException("Cart doesn't exist");
            }
            cart.Discount = rnd.Next(1, 100);
            UpdateCartProducts(products, cart);
            GetCartTotal(cart);
            _cartRepository.AddProductsInCart(cartId);
            return cart;
        }

        public Cart GetCart(int cartId)
        {
            return _cartRepository.GetCart(cartId);
        }

        public CartProduct GetProductFromCart(int productId, int cartId)
        {
            return _cartRepository.GetProductFromCart(productId, cartId);
        }

        private void UpdateCartProducts(List<CartProduct> products, Cart cart)
        {
            
            List<CartProduct> cartProducts = _cartRepository.GetCartProducts(cart.CartId);
            foreach (CartProduct prod in products)
            {
                int productInCartIndex = cartProducts.FindIndex(product => product.ProductId == prod.ProductId);
                if (productInCartIndex == -1)
                {
                    cartProducts.Add(prod);
                    _cartRepository.AddCartProduct(prod);
                }
                else
                {
                    prod.Quantity = cartProducts[productInCartIndex].Quantity + prod.Quantity;
                }
            }
            cart.Products = cartProducts;
        }

        private void GetCartTotal(Cart cart)
        {
            decimal cartTotal = 0;
            foreach (CartProduct productDetails in cart.Products)
            {
                cartTotal = cartTotal + productDetails.Price * productDetails.Quantity;
            }
            cart.Total =  cartTotal - cartTotal * cart.Discount / 100;
        }
    }
}
