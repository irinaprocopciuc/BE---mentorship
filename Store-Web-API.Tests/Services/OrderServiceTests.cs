using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store_Web_API.Models;
using Store_Web_API.Repositories.Interfaces;
using Store_Web_API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_Web_API.Tests.Services
{
    [TestClass]
    public class OrderServiceTests
    {

        private ICartRepository cartRepository;
        private IOrderRepository orderRepository;
        private IOrderService sut;

        [TestInitialize]
        public void TestInitialize()
        {
            cartRepository = A.Fake<ICartRepository>();
            orderRepository = A.Fake<IOrderRepository>();
            sut = new OrderService(orderRepository, cartRepository);
        }


        [TestMethod]
        public void Given_GetOrders_When_NoErrorOccurs_Then_ShouldReturnAListOfOrders()
        {
            //Arrange
            List<Order> orders = new List<Order>
            {
                new Order
                {
                    OrderId = 1,
                    CartId = 3
                },
                new Order
                {
                    OrderId = 2,
                    CartId = 7
                }
            };
            A.CallTo(() => orderRepository.GetOrders()).Returns(orders);

            //Act
            sut.GetOrders();

            //Assert
            A.CallTo(() => orderRepository.GetOrders()).MustHaveHappenedOnceExactly();
            A.CallTo(() => cartRepository.GetCart(A<int>._)).MustHaveHappened();
            A.CallTo(() => cartRepository.GetCartProducts(A<int>._)).MustHaveHappened();
        }

        [TestMethod]
        public void Given_AddOrder_When_OrderISProvided_Then_ShouldAddANewOrder()
        {
            //Arrange
           var order = new Order
            {
                OrderId = 1,
                CartId = 3
            };

            //Act
            sut.AddOrder(order);

            //Assert
            A.CallTo(() => orderRepository.AddOrder(order)).MustHaveHappenedOnceExactly();
        }
    }
}
