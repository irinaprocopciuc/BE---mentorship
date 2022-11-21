
using StoreWebAPI.Domain.Entities;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Store_Web_API.IntegrationTests
{
    public class CartControllerIntegrationTest: TestBase
    {
        public CartControllerIntegrationTest(AppFixture factory): base(factory) { }


        private static List<T> CreateList<T>(params T[] elements)
        {
            return new List<T>(elements);
        }

        [Theory]
        [InlineData("/store-api/carts/{0}")]
        public async Task GET_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            //Arrange
            var client = Fixture.CreateClient();
            var cartResponse = await Fixture.Add(new Cart(new List<CartProduct>()));
            var sendUrl = string.Format(url, cartResponse.Id);

            //Act
            var response = await client.GetAsync(sendUrl);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task POST_ReturnsCartWithNewProductAdded()
        {
            //Arrange
            var client = Fixture.CreateClient();
            var productRes = await Fixture.Add(new Product(0, "Test 2", "Descr test 2", 41, 100, "makeup"));
            var cartRes = await Fixture.Add((new Cart(new List<CartProduct>())));
            var postRequest = new HttpRequestMessage(HttpMethod.Post, $"/store-api/carts/{cartRes.Id}/products");
            postRequest.Content = new StringContent(JsonSerializer.Serialize(
                CreateList( 
                    new 
                    {
                        ProductId = productRes.Id,
                        Name = "Test",
                        Price = 34,
                        Quantity = 2,
                        CartId = cartRes.Id
                    } 
                )), Encoding.UTF8, "application/json");

            //Act
            var response = await client.SendAsync(postRequest);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task POST_ReturnsNewCart()
        {
            //Arrange
            var client = Fixture.CreateClient();
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/store-api/carts/cart");
            postRequest.Content = new StringContent(JsonSerializer.Serialize(
                new
                {
                    Discount = 20,
                    Total = 100
                }), Encoding.UTF8, "application/json");

            //Act
            var response = await client.SendAsync(postRequest);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task POST_ReturnsNotFound_WhenCartAlreadyExists()
        {
            //Arrange
            var client = Fixture.CreateClient();
            var cartRes = await Fixture.Add((new Cart(new List<CartProduct>() )));
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/store-api/carts/cart");
            postRequest.Content = new StringContent(JsonSerializer.Serialize(
                new
                {
                    CartId = cartRes.Id,
                    Discount = 20,
                    Total = 100
                }), Encoding.UTF8, "application/json");

            //Act
            var response = await client.SendAsync(postRequest);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task DELETE_ReturnsNotFound_WhenProductIsNotFoundInCart()
        {
            //Arrange
            var client = Fixture.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Delete, "/store-api/carts/2/2");

            //Act
            var response = await client.SendAsync(request);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }


        [Fact]
        public async Task DELETE_ReturnsNoContent_WhenProuctIsDeletedFromCart()
        {
            //Arrange
            var client = Fixture.CreateClient();
            var cartRes = await Fixture.Add((new Cart(new List<CartProduct>())));
            var productRes = await Fixture.Add(new Product(0,"Test 2", "Descr test 2", 41, 100, "makeup"));
            await Fixture.Add(new CartProduct(0, productRes.Id, new Product(0, "Test 2", "Descr test 2", 41, 100, "makeup"), "Test",  34,  2, cartRes.Id ));
            var request = new HttpRequestMessage(HttpMethod.Delete, $"/store-api/carts/{productRes.Id}/{cartRes.Id}");

            //Act
            var response = await client.SendAsync(request);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}