using AutoFixture;
using DeployAWS.Application.Dtos;
using DeployAWS.Application.Interfaces;
using FluentAssertions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DeployAWS.MSTest.Customer.Controller
{
    [TestClass]
    public class CustomerControllerTests
    {
        private static Fixture _fixture;
        private readonly Mock<IApplicationServiceCustomer> _mockApplicationServiceCustomer;
        private readonly Mock<IValidator<CustomerDto>> validatorCustomer;

        public CustomerControllerTests() 
        {
            _fixture = new Fixture();
            _mockApplicationServiceCustomer = new Mock<IApplicationServiceCustomer>();
            validatorCustomer = new Mock<IValidator<CustomerDto>>();
        }

        [TestMethod]
        public async Task CustomerController_GetAsync_ShouldReturn_NotNull()
        {
            // Arrange
            _mockApplicationServiceCustomer.Setup(c => c.GetAllAsync()).ReturnsAsync(MockListCustomerDto());
            var controllerMock = new API.Controllers.CustomerController(_mockApplicationServiceCustomer.Object, validatorCustomer.Object);

            // Act
            var result = await controllerMock.GetAsync();

            // Assert
            result.Should().NotBeNull();
        }
        [TestMethod]
        public async Task CustomerController_GetAsync_ShouldReturn_StatusCode_200()
        {
            // Arrange
            _mockApplicationServiceCustomer.Setup(c => c.GetAllAsync()).ReturnsAsync(MockListCustomerDto());
            var controllerMock = new API.Controllers.CustomerController(_mockApplicationServiceCustomer.Object, validatorCustomer.Object);

            // Act
            var result = await controllerMock.GetAsync();

            // Assert
            var actionResult = (OkObjectResult)result;
            actionResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }
        [TestMethod]
        public async Task CustomerController_GetByIdAsync_ShouldReturn_NotNull()
        {
            // Arrange
            string mockId = Guid.NewGuid().ToString();
            _mockApplicationServiceCustomer.Setup(c => c.GetByIdAsync(mockId)).ReturnsAsync(MockListCustomerDto().First());
            var controllerMock = new API.Controllers.CustomerController(_mockApplicationServiceCustomer.Object, validatorCustomer.Object);

            // Act
            var result = await controllerMock.GetAsync();

            // Assert
            result.Should().NotBeNull();
        }
        [TestMethod]
        public async Task CustomerController_GetByIdAsync_ShouldReturn_StatusCode_200()
        {
            // Arrange
            string mockId = Guid.NewGuid().ToString();
            _mockApplicationServiceCustomer.Setup(c => c.GetByIdAsync(mockId)).ReturnsAsync(MockListCustomerDto().First());
            var controllerMock = new API.Controllers.CustomerController(_mockApplicationServiceCustomer.Object, validatorCustomer.Object);

            // Act
            var result = await controllerMock.GetAsync();

            // Assert
            var actionResult = (OkObjectResult)result;
            actionResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }
        [TestMethod]
        public void CustomerController_Post_ShouldReturn_StatusCode_404()
        {
            // Arrange
            _mockApplicationServiceCustomer.Setup(c => c.Update(new CustomerDto()));
            var controllerMock = new API.Controllers.CustomerController(_mockApplicationServiceCustomer.Object, validatorCustomer.Object);

            // Act
            var result = controllerMock.Post(null);

            // Assert
            var actionResult = (NotFoundResult)result;
            actionResult.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }
        [TestMethod]
        public void CustomerController_Post_ShouldReturn_StatusCode_500()
        {
            // Arrange
            _mockApplicationServiceCustomer.Setup(c => c.Update(new CustomerDto()));
            var controllerMock = new API.Controllers.CustomerController(_mockApplicationServiceCustomer.Object, validatorCustomer.Object);

            // Act
            var result = controllerMock.Post(new CustomerDto());

            // Assert
            var actionResult = (ObjectResult)result;
            actionResult.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        }
        [TestMethod]
        public void CustomerController_Put_ShouldReturn_StatusCode_404()
        {
            // Arrange
            _mockApplicationServiceCustomer.Setup(c => c.Update(new CustomerDto()));
            var controllerMock = new API.Controllers.CustomerController(_mockApplicationServiceCustomer.Object, validatorCustomer.Object);

            // Act
            var result = controllerMock.Put(null);

            // Assert
            var actionResult = (NotFoundResult)result;
            actionResult.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }
        [TestMethod]
        public void CustomerController_Put_ShouldReturn_StatusCode_500()
        {
            // Arrange
            _mockApplicationServiceCustomer.Setup(c => c.Update(new CustomerDto()));
            var controllerMock = new API.Controllers.CustomerController(_mockApplicationServiceCustomer.Object, validatorCustomer.Object);

            // Act
            var result = controllerMock.Post(new CustomerDto());

            // Assert
            var actionResult = (ObjectResult)result;
            actionResult.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        }
        private IEnumerable<CustomerDto> MockListCustomerDto()
        {
            var CustomersDto = _fixture.Build<CustomerDto>()
                .CreateMany(10);
            return CustomersDto;
        }
    }
}
