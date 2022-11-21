using StoreWebApi.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace StoreWebAPI.Domain.Entities
{
    public class Cart: BaseEntity
    {
        public IReadOnlyCollection<CartProduct> Products => _products;
        public int Discount { get;  private set;  }
        public decimal Total {get; private set;}

        private List<CartProduct> _products;

        public Cart(): base(0)
        {
            _products = new List<CartProduct>();
        }

        public Cart(IReadOnlyCollection<CartProduct> products) : base(0)
        {
            _products.AddRange(products);
            SetDiscount();
            CalculateTotal();
        }

        public void AddProductsInCart(List<CartProduct> productsToAdd)
        {
            foreach (CartProduct product in productsToAdd)
            {
                int productInCartIndex = _products.FindIndex(cartProduct => cartProduct.ProductId == product.ProductId);
                if (productInCartIndex == -1)
                {
                    _products.Add(product);
                }
                else
                {
                    product.ChangeQuantity(_products[productInCartIndex].Quantity + product.Quantity);
                }
            }
            SetDiscount();
            CalculateTotal();
        }

        private void SetDiscount()
        {
            var total = Products.Sum(p => p.Price * p.Quantity);
            switch (total)
            {
                case < 250:
                    Discount = 0;
                    break;
                case > 250:
                    Discount = 10;
                    break;
            }
        }

        private void CalculateTotal()
        {
            var total = Products.Sum(p => p.Price * p.Quantity);
            Total = total - total * Discount / 100;
        }
    }
}
