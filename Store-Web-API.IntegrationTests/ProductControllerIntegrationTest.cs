
using Microsoft.EntityFrameworkCore.Storage;
using Respawn;
using Store_Web_API.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Store_Web_API.IntegrationTests
{
    public class ProductControllerIntegrationTest: TestBase
    {
        public ProductControllerIntegrationTest(AppFixture factory): base(factory) { }
        

        [Theory]
        [InlineData("/store-api/products")]
        [InlineData("/store-api/products/{0}")]
        public async Task GET_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            //Arrange
            var client = Fixture.CreateClient();
            await Fixture.Add(new Product { Name = "Test", Description = "Descr test", Stock = 23, Price = 34, Category = "makeup" });
            var productRes = await Fixture.Add(new Product { Name = "Test 2", Description = "Descr test 2", Stock = 41, Price = 100, Category = "makeup" });
            var sendUrl = string.Format(url, productRes.ProductId);

            //Act
            var response = await client.GetAsync(sendUrl);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task GET_ReturnsNotFound_WhenProductIdIsNotInTheTable()
        {
            //Arrange
            var client = Fixture.CreateClient();

            //Act
            var response = await client.GetAsync("/store-api/products/3");

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }


        [Fact]
        public async Task POST_ReturnsCreatedStatusCode()
        {
            //Arrange
            var client = Fixture.CreateClient();
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/store-api/products");
            postRequest.Content = new StringContent(JsonSerializer.Serialize(
                new {
                    Name = "Test",
                    Description = "Descr test",
                    Stock = 23,
                    Price = 34,
                    Category = "makeup" 
                }), Encoding.UTF8, "application/json");

            //Act
            var response = await client.SendAsync(postRequest);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task POST_ReturnsNotFoundStatusCode_WhenIdAlreadyExists()
        {
            //Arrange
            var client = Fixture.CreateClient();
            var productRes = await Fixture.Add(new Product { Name = "Test", Description = "Descr test 2", Stock = 23, Price = 34 , Category = "makeup" });
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/store-api/products");
            postRequest.Content = new StringContent(JsonSerializer.Serialize(
                new
                {
                    ProductId = productRes.ProductId,
                    Name = "Test",
                    Description = "Descr test",
                    Stock = 23,
                    Price = 34,
                    Category = "makeup"
                }), Encoding.UTF8, "application/json");

            //Act
            var response = await client.SendAsync(postRequest);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task PUT_ReturnsNoContent_WhenProductUpdatedSuccessfully()
        {
            //Arrange
            var client = Fixture.CreateClient();
            var productRes = await Fixture.Add(new Product { Name = "Test 2", Description = "Descr test 2", Stock = 41, Price = 100, Category = "makeup" });
            var putRequest = new HttpRequestMessage(HttpMethod.Put, "/store-api/products");
            putRequest.Content = new StringContent(JsonSerializer.Serialize(
                new
                {
                    ProductId = productRes.ProductId,
                    Name = "Test new",
                    Description = "Descr test",
                    Stock = 23,
                    Price = 34,
                    Category = "makeup"
                }), Encoding.UTF8, "application/json");

            //Act
            var response = await client.SendAsync(putRequest);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task PUT_ReturnsNotFound_WhenProductDoesntExist()
        {
            //Arrange
            var client = Fixture.CreateClient();
            await Fixture.Add(new Product { Name = "Test 2", Description = "Descr test 2", Stock = 41, Price = 100, Category = "makeup" });
            var putRequest = new HttpRequestMessage(HttpMethod.Put, "/store-api/products");
            putRequest.Content = new StringContent(JsonSerializer.Serialize(
                new
                {
                    ProductId = 101,
                    Name = "Test new",
                    Description = "Descr test",
                    Stock = 23,
                    Price = 34,
                    Category = "makeup"
                }), Encoding.UTF8, "application/json");

            //Act
            var response = await client.SendAsync(putRequest);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task DELETE_ReturnsNoContent_WhenProductIsDeleted()
        {
            //Arrange
            var client = Fixture.CreateClient();
            var productRes = await Fixture.Add(new Product { Name = "Test 2", Description = "Descr test 2", Stock = 41, Price = 100, Category = "makeup" });
            var request = new HttpRequestMessage(HttpMethod.Delete, $"/store-api/products/{productRes.ProductId}");

            //Act
            var response = await client.SendAsync(request);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task DELETE_ReturnsNotFound_WhenProductDoesntExist()
        {
            //Arrange
            var client = Fixture.CreateClient();
            await Fixture.Add(new Product { Name = "Test 2", Description = "Descr test 2", Stock = 41, Price = 100, Category = "makeup" });
            var request = new HttpRequestMessage(HttpMethod.Delete, "/store-api/products/55");

            //Act
            var response = await client.SendAsync(request);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
