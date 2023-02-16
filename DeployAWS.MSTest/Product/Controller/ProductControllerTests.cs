using Amazon.Runtime.Internal.Util;
using AutoFixture;
using DeployAWS.API.Controllers;
using DeployAWS.Application.Dtos;
using DeployAWS.Application.Interfaces;
using FluentAssertions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DeployAWS.MSTest.Product.Controller
{
    [TestClass]
    public class ProductControllerTests
    {
        private static Fixture _fixture;
        private readonly Mock<IApplicationServiceProduct> _mockApplicationServiceProduct;
        private readonly Mock<IValidator<ProductDto>> _validatorProduct;
        private readonly Mock<ILogger<ProductController>> _logger;

        public ProductControllerTests()
        {
            _fixture = new Fixture();
            _mockApplicationServiceProduct = new Mock<IApplicationServiceProduct>();
            _validatorProduct = new Mock<IValidator<ProductDto>>();
            _logger = new Mock<ILogger<ProductController>>();
        }

        [TestMethod]
        public async Task ProductController_GetAsync_ShouldReturn_NotNull()
        {
            // Arrange
            _mockApplicationServiceProduct.Setup(c => c.GetAllAsync()).ReturnsAsync(MockListProductDto());
            var controllerMock = new ProductController(_mockApplicationServiceProduct.Object, _validatorProduct.Object,
                _logger.Object);

            // Act
            var result = await controllerMock.GetAsync();

            // Assert
            result.Should().NotBeNull();
        }
        [TestMethod]
        public async Task ProductController_GetAsync_ShouldReturn_StatusCode_200()
        {
            // Arrange
            _mockApplicationServiceProduct.Setup(c => c.GetAllAsync()).ReturnsAsync(MockListProductDto());
            var controllerMock = new ProductController(_mockApplicationServiceProduct.Object, _validatorProduct.Object,
                _logger.Object);

            // Act_mockApplicationServiceProduct
            var result = await controllerMock.GetAsync();

            // Assert
            var actionResult = (OkObjectResult)result;
            actionResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }
        [TestMethod]
        public async Task ProductController_GetByIdAsync_ShouldReturn_NotNull()
        {
            // Arrange
            var mockId = Guid.NewGuid().ToString();
            _mockApplicationServiceProduct.Setup(c => c.GetByIdAsync(mockId)).ReturnsAsync(MockListProductDto().First());
            var controllerMock = new ProductController(_mockApplicationServiceProduct.Object, _validatorProduct.Object,
                _logger.Object);

            // Act
            var result = await controllerMock.GetAsync();

            // Assert
            result.Should().NotBeNull();
        }
        [TestMethod]
        public async Task ProductController_GetByIdAsync_ShouldReturn_StatusCode_200()
        {
            // Arrange
            var mockId = Guid.NewGuid().ToString();
            _mockApplicationServiceProduct.Setup(c => c.GetByIdAsync(mockId)).ReturnsAsync(MockListProductDto().First());
            var controllerMock = new ProductController(_mockApplicationServiceProduct.Object, _validatorProduct.Object,
                _logger.Object);

            // Act_mockApplicationServiceProduct
            var result = await controllerMock.GetAsync();

            // Assert
            var actionResult = (OkObjectResult)result;
            actionResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }
        [TestMethod]
        public void ClientController_Post_ShouldReturn_StatusCode_404()
        {
            // Arrange
            _mockApplicationServiceProduct.Setup(c => c.Update(new ProductDto()));
            var controllerMock = new ProductController(_mockApplicationServiceProduct.Object, _validatorProduct.Object,
                _logger.Object);

            // Act
            var result = controllerMock.Post(null);

            // Assert
            var actionResult = (NotFoundResult)result;
            actionResult.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }
        [TestMethod]
        public void ClientController_Post_ShouldReturn_StatusCode_500()
        {
            // Arrange
            _mockApplicationServiceProduct.Setup(c => c.Update(new ProductDto()));
            var controllerMock = new ProductController(_mockApplicationServiceProduct.Object, _validatorProduct.Object,
                _logger.Object);

            // Act
            var result = controllerMock.Post(new ProductDto());

            // Assert
            var actionResult = (ObjectResult)result;
            actionResult.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        }
        [TestMethod]
        public void ClientController_Put_ShouldReturn_StatusCode_404()
        {
            // Arrange
            _mockApplicationServiceProduct.Setup(c => c.Update(new ProductDto()));
            var controllerMock = new ProductController(_mockApplicationServiceProduct.Object, _validatorProduct.Object,
                _logger.Object);

            // Act
            var result = controllerMock.Put(null);

            // Assert
            var actionResult = (NotFoundResult)result;
            actionResult.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }
        [TestMethod]
        public void ClientController_Put_ShouldReturn_StatusCode_500()
        {
            // Arrange
            _mockApplicationServiceProduct.Setup(c => c.Update(new ProductDto()));
            var controllerMock = new ProductController(_mockApplicationServiceProduct.Object, _validatorProduct.Object,
                _logger.Object);

            // Act
            var result = controllerMock.Post(new ProductDto());

            // Assert
            var actionResult = (ObjectResult)result;
            actionResult.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        }
        private IEnumerable<ProductDto> MockListProductDto()
        {
            var productsDto = _fixture.Build<ProductDto>()
                .CreateMany(10);
            return productsDto;
        }
    }
}
