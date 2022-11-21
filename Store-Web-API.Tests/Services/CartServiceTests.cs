using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Store_Web_API.Tests
{
    using FakeItEasy;
    using Store_Web_API.DataAccess;
    using Store_Web_API.Filters.Custom_Exceptions;
    using Store_Web_API.Models;
    using Store_Web_API.Repositories;
    using Store_Web_API.Repositories.Interfaces;
    using Store_Web_API.Services;
    using System;
    using System.Collections.Generic;

    [TestClass]
    public class CartServiceTests
    {
        private ICartRepository cartRepository;
        private ICartService sut;

        [TestInitialize]
        public void TestInitialize()
        {
            cartRepository = A.Fake<ICartRepository>();
            sut = new CartService(cartRepository);
        }

        [TestMethod]
        public void Given_RemoveProductFromCart_When_ProductIsNotFound_Then_ShouldThrowNotFoundError()
        {
            //Arrange
            var prodId = 3;
            var cartId = 1;
            A.CallTo(() => cartRepository.GetProductFromCart(prodId, cartId)).Returns(null);

            //Act
            Action act = () => sut.RemoveProductFromCart(prodId, cartId);

            //Assert
            Assert.ThrowsException<NotFoundDomainException>(act);
            A.CallTo(() => cartRepository.GetCart(A<int>._)).MustNotHaveHappened();
            A.CallTo(() => cartRepository.RemoveProductFromCart(A<CartProduct>._)).MustNotHaveHappened();
        }

        [TestMethod]
        public void Given_RemoveProductFromCart_When_CartIsNotFound_Then_ShouldThrowNotFoundError()
        {
            //Arrange
            var prodId = 3;
            var cartId = 1;
            A.CallTo(() => cartRepository.GetCart(cartId)).Returns(null);

            //Act
            Action act = () => sut.RemoveProductFromCart(prodId, cartId);

            //Assert
            Assert.ThrowsException<NotFoundDomainException>(act);
            A.CallTo(() => cartRepository.GetProductFromCart(prodId, cartId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => cartRepository.GetCart(cartId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => cartRepository.RemoveProductFromCart(A<CartProduct>._)).MustNotHaveHappened();
        }

        [TestMethod]
        public void Given_RemoveProductFromCart_When_ProductANdCartAreProvided_Then_ShouldDeleteProductFromCart()
        {
            //Arrange
            var prodId = 3;
            var cartId = 1;
            var cart = new Cart
            {
                CartId = 1,
                Products = { },
                Discount = 50,
                Total = 212
            };
            var cartProd = new CartProduct
            {
                Id = 3,
                ProductId = 3,
                CartId = 1
            };
            A.CallTo(() => cartRepository.GetProductFromCart(prodId, cartId)).Returns(cartProd);
            A.CallTo(() => cartRepository.GetCart(cartId)).Returns(cart);

            //Act
            sut.RemoveProductFromCart(prodId, cartId);

            //Assert
            A.CallTo(() => cartRepository.GetProductFromCart(prodId, cartId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => cartRepository.GetCart(cartId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => cartRepository.RemoveProductFromCart(cartProd)).MustHaveHappenedOnceExactly();
        }

        [TestMethod]
        public void Given_Add_When_CartIsProvided_Then_ShouldAddNewCart()
        {
            //Arrange
            var cart = new Cart
            {
                CartId = 3,
                Products = { },
                Discount = 50,
                Total = 212
            };

            //Act
            var response = sut.Add(cart);

            //Assert
            Assert.IsInstanceOfType(response, typeof(Cart));
            A.CallTo(() => cartRepository.Add(cart)).MustHaveHappenedOnceExactly();
        }

        [TestMethod]
        public void Given_AddProductsInCart_When_CartIsNotFound_Then_ShouldThrowNotFoundException()
        {
            //Arrange
            var cartId = 1;
            var products = new List<CartProduct>();
            A.CallTo(() => cartRepository.GetCart(cartId)).Returns(null);

            //Act
            Action act = () => sut.AddProductsInCart(products, cartId);

            //Assert
            Assert.ThrowsException<NotFoundDomainException>(act);
            A.CallTo(() => cartRepository.GetCart(cartId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => cartRepository.AddProductsInCart(A<int>._)).MustNotHaveHappened();
        }

        [TestMethod]
        public void Given_AddProductsInCart_When_ListOfProductsAndCartIdAreProvided_Then_ShouldAddProductsInCart()
        {
            //Arrange
            var cartId = 1;
            var cart = new Cart
            {
                CartId = 1,
                Products = { },
                Discount = 50,
                Total = 212
            };
            var products = new List<CartProduct>()
            {
                new CartProduct
                {
                    Id = 3,
                    ProductId = 3,
                    CartId = 1,
                    Name = "test1",
                    Price = 100
                },
                new CartProduct
                {
                    Id = 4,
                    ProductId = 4,
                    CartId = 1,
                    Name = "test2",
                    Price = 100
                }
            };
            A.CallTo(() => cartRepository.GetCart(cartId)).Returns(cart);

            //Act
            sut.AddProductsInCart(products, cartId);

            //Assert
            A.CallTo(() => cartRepository.GetCart(cartId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => cartRepository.AddProductsInCart(cartId)).MustHaveHappenedOnceExactly();
        }

        public void Given_GetCart_When_CartIsProvided_Then_ShouldGetCartDetails()
        {
            //Arrange
            var cartId = 1;

            //Act
            var response = sut.GetCart(cartId);

            //Assert
            A.CallTo(() => cartRepository.GetCart(cartId)).MustHaveHappenedOnceExactly();
        }

        public void Given_GetProductFromCart_When_CartIdAndproductIdAreProvided_Then_ShouldGetProductFromCart()
        {
            //Arrange
            var cartId = 1;
            var productId = 3;

            //Act
            var response = sut.GetProductFromCart(productId, cartId);

            //Assert
            A.CallTo(() => cartRepository.GetProductFromCart(productId, cartId)).MustHaveHappenedOnceExactly();
        }
    }
}
