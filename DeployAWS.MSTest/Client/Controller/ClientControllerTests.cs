using AutoFixture;
using DeployAWS.Application.Dtos;
using DeployAWS.Application.Interfaces;
using FluentAssertions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DeployAWS.MSTest.Client.Controller
{
    [TestClass]
    public class ClientControllerTests
    {
        private static Fixture _fixture;
        private readonly Mock<IApplicationServiceClient> _mockApplicationServiceClient;
        private readonly Mock<IValidator<ClientDto>> _validatorClient;

        public ClientControllerTests() 
        {
            _fixture = new Fixture();
            _mockApplicationServiceClient = new Mock<IApplicationServiceClient>();
            _validatorClient = new Mock<IValidator<ClientDto>>();
        }

        [TestMethod]
        public async Task ClientController_GetAsync_ShouldReturn_NotNull()
        {
            // Arrange
            _mockApplicationServiceClient.Setup(c => c.GetAllAsync()).ReturnsAsync(MockListClientDto());
            var controllerMock = new API.Controllers.ClientController(_mockApplicationServiceClient.Object, _validatorClient.Object);

            // Act
            var result = await controllerMock.GetAsync();

            // Assert
            result.Should().NotBeNull();
        }
        [TestMethod]
        public async Task ClientController_GetAsync_ShouldReturn_StatusCode_200()
        {
            // Arrange
            _mockApplicationServiceClient.Setup(c => c.GetAllAsync()).ReturnsAsync(MockListClientDto());
            var controllerMock = new API.Controllers.ClientController(_mockApplicationServiceClient.Object, _validatorClient.Object);

            // Act
            var result = await controllerMock.GetAsync();

            // Assert
            var actionResult = (OkObjectResult)result;
            actionResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }
        [TestMethod]
        public async Task ClientController_GetByIdAsync_ShouldReturn_NotNull()
        {
            // Arrange
            var mockId = 10;
            _mockApplicationServiceClient.Setup(c => c.GetByIdAsync(mockId)).ReturnsAsync(MockListClientDto().First());
            var controllerMock = new API.Controllers.ClientController(_mockApplicationServiceClient.Object, _validatorClient.Object);

            // Act
            var result = await controllerMock.GetAsync();

            // Assert
            result.Should().NotBeNull();
        }
        [TestMethod]
        public async Task ClientController_GetByIdAsync_ShouldReturn_StatusCode_200()
        {
            // Arrange
            var mockId = 10;
            _mockApplicationServiceClient.Setup(c => c.GetByIdAsync(mockId)).ReturnsAsync(MockListClientDto().First());
            var controllerMock = new API.Controllers.ClientController(_mockApplicationServiceClient.Object, _validatorClient.Object);

            // Act
            var result = await controllerMock.GetAsync();

            // Assert
            var actionResult = (OkObjectResult)result;
            actionResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }
        [TestMethod]
        public void ClientController_Post_ShouldReturn_StatusCode_404()
        {
            // Arrange
            _mockApplicationServiceClient.Setup(c => c.Update(new ClientDto()));
            var controllerMock = new API.Controllers.ClientController(_mockApplicationServiceClient.Object, _validatorClient.Object);

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
            _mockApplicationServiceClient.Setup(c => c.Update(new ClientDto()));
            var controllerMock = new API.Controllers.ClientController(_mockApplicationServiceClient.Object, _validatorClient.Object);

            // Act
            var result = controllerMock.Post(new ClientDto());

            // Assert
            var actionResult = (ObjectResult)result;
            actionResult.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        }
        [TestMethod]
        public void ClientController_Put_ShouldReturn_StatusCode_404()
        {
            // Arrange
            _mockApplicationServiceClient.Setup(c => c.Update(new ClientDto()));
            var controllerMock = new API.Controllers.ClientController(_mockApplicationServiceClient.Object, _validatorClient.Object);

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
            _mockApplicationServiceClient.Setup(c => c.Update(new ClientDto()));
            var controllerMock = new API.Controllers.ClientController(_mockApplicationServiceClient.Object, _validatorClient.Object);

            // Act
            var result = controllerMock.Post(new ClientDto());

            // Assert
            var actionResult = (ObjectResult)result;
            actionResult.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        }
        private IEnumerable<ClientDto> MockListClientDto()
        {
            var clientsDto = _fixture.Build<ClientDto>()
                .CreateMany(10);
            return clientsDto;
        }
    }
}
