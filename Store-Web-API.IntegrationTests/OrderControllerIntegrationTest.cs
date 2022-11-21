using Store_Web_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Store_Web_API.IntegrationTests
{
    public class OrderControllerIntegrationTest : TestBase
    {
        public OrderControllerIntegrationTest(AppFixture factory) : base(factory) { }

        [Theory]
        [InlineData("/store-api/orders")]
        public async Task GET_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            //Arrange
            var client = Fixture.CreateClient();
            var cartRes = await Fixture.Add(new Cart { Discount = 23, Products = { }, Total = 100 });
            await Fixture.Add(new Order { CartId = cartRes.CartId, Adress = "new adress", Name = "client", PaymentMethod ="cash", Phone = "000-9998" });

            //Act
            var response = await client.GetAsync(url);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }


        [Fact]
        public async Task POST_ReturnsCreated_WhenOrderISAddedToTheDatabase()
        {
            //Arrange
            var client = Fixture.CreateClient();
            var cartRes = await Fixture.Add(new Cart { Discount = 23, Products = { }, Total = 100 });
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/store-api/orders");
            postRequest.Content = new StringContent(JsonSerializer.Serialize(
                new
                {
                    CartId = cartRes.CartId,
                    Name = "client 1",
                    Adress = "str. 34",
                    Phone = "000988",
                    PaymentMethod = "cash"
                }), Encoding.UTF8, "application/json");

            //Act
            var response = await client.SendAsync(postRequest);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task POST_ReturnsNotFound_WhenOrderAlreadyExists()
        {
            //Arrange
            var client = Fixture.CreateClient();
            var cartRes = await Fixture.Add(new Cart { Discount = 23, Products = { }, Total = 100 });
            var orderRes = await Fixture.Add(new Order { CartId = cartRes.CartId, Adress = "new adress", Name = "client", PaymentMethod = "cash", Phone = "000-9998" });
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/store-api/orders");
            postRequest.Content = new StringContent(JsonSerializer.Serialize(
                new
                {
                    OrderId = orderRes.OrderId,
                    CartId = 1,
                    Name = "client 1",
                    Adress = "str. 34",
                    Phone = "000988",
                    PaymentMethod = "cash"
                }), Encoding.UTF8, "application/json");

            //Act
            var response = await client.SendAsync(postRequest);

            //Assert
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }
    }
}
