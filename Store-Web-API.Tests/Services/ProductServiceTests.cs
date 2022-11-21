using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store_Web_API.Filters.Custom_Exceptions;
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
    public class ProductServiceTests
    {
        private IProductRepository productRepository;
        private IProductService sut;

        [TestInitialize]
        public void TestInitialize()
        {
            productRepository = A.Fake<IProductRepository>();
            sut = new ProductService(productRepository);
        }


        [TestMethod]
        public void Given_Add_When_ProductIsAlreadyExisting_Then_ShouldThrowNotFoundError()
        {
            //Arrange
            var prodId = 3;
            Product product = new Product
            {
                ProductId = 3,
                Name = "Test"
            };
            A.CallTo(() => productRepository.GetById(prodId)).Returns(product);

            //Act
            Action act = () => sut.Add(product);

            //Assert
            Assert.ThrowsException<NotFoundDomainException>(act);
            A.CallTo(() => productRepository.GetById(prodId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => productRepository.Add(A<Product>._)).MustNotHaveHappened();
        }

        [TestMethod]
        public void Given_Add_When_ProductIsNew_Then_ShouldAddProduct()
        {
            //Arrange
            var prodId = 3;
            Product product = new Product
            {
                ProductId = 3,
                Name = "Test"
            };
            A.CallTo(() => productRepository.GetById(prodId)).Returns(null);

            //Act
            sut.Add(product);

            //Assert
            A.CallTo(() => productRepository.GetById(prodId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => productRepository.Add(A<Product>._)).MustHaveHappenedOnceExactly();
        }

        [TestMethod]
        public void Given_GetAll_When_NoErrorOccurs_Then_GetTheListOfProducts()
        {
            //Arrange

            //Act
            sut.GetAll();

            //Assert
            A.CallTo(() => productRepository.GetAll()).MustHaveHappenedOnceExactly();
        }

        [TestMethod]
        public void Given_GetById_When_ProductIsNotFound_Then_ShouldThrowNotFoundError()
        {
            //Arrange
            var prodId = 3;
            A.CallTo(() => productRepository.GetById(prodId)).Returns(null);

            //Act
            Action act = () => sut.GetById(prodId);

            //Assert
            Assert.ThrowsException<NotFoundDomainException>(act);
            A.CallTo(() => productRepository.GetById(prodId)).MustHaveHappenedOnceExactly();
        }

        [TestMethod]
        public void Given_GetById_When_ProductIdIsProvided_Then_ShouldGetProductById()
        {
            //Arrange
            var prodId = 3;
            Product product = new Product
            {
                ProductId = 3,
                Name = "Test"
            };
            A.CallTo(() => productRepository.GetById(prodId)).Returns(product);

            //Act
            sut.GetById(prodId);

            //Assert
            A.CallTo(() => productRepository.GetById(prodId)).MustHaveHappenedOnceExactly();
        }

        [TestMethod]
        public void Given_Remove_When_ProductIsNotFound_Then_ShouldThrowNotFoundError()
        {
            //Arrange
            var prodId = 3;
            A.CallTo(() => productRepository.GetById(prodId)).Returns(null);

            //Act
            Action act = () => sut.Remove(prodId);

            //Assert
            Assert.ThrowsException<NotFoundDomainException>(act);
            A.CallTo(() => productRepository.GetById(prodId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => productRepository.Remove(prodId)).MustNotHaveHappened();
        }

        [TestMethod]
        public void Given_Remove_When_ProductIdIsProvided_Then_ShouldRemoveProduct()
        {
            //Arrange
            var prodId = 3;
            Product product = new Product
            {
                ProductId = 3,
                Name = "Test"
            };
            A.CallTo(() => productRepository.GetById(prodId)).Returns(product);

            //Act
            sut.Remove(prodId);

            //Assert
            A.CallTo(() => productRepository.GetById(prodId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => productRepository.Remove(prodId)).MustHaveHappenedOnceExactly();
        }

        [TestMethod]
        public void Given_Update_When_ProductIsNotFound_Then_ShouldThrowNotFoundError()
        {
            //Arrange
            var prodId = 3;
            Product product = new Product
            {
                ProductId = 3,
                Name = "Tes2t"
            };
            A.CallTo(() => productRepository.GetById(prodId)).Returns(null);

            //Act
            Action act = () => sut.Update(product);

            //Assert
            Assert.ThrowsException<NotFoundDomainException>(act);
            A.CallTo(() => productRepository.GetById(prodId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => productRepository.Update(product)).MustNotHaveHappened();
        }

        [TestMethod]
        public void Given_Update_When_ProductIdIsProvided_Then_ShouldUpdateProduct()
        {
            //Arrange
            var prodId = 3;
            Product product = new Product
            {
                ProductId = 3,
                Name = "Test"
            };
            A.CallTo(() => productRepository.GetById(prodId)).Returns(product);

            //Act
            sut.Update(product);

            //Assert
            A.CallTo(() => productRepository.GetById(prodId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => productRepository.Update(product)).MustHaveHappenedOnceExactly();
        }
    }
}
