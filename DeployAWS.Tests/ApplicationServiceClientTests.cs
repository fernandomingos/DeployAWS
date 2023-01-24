using AutoFixture;
using AutoMapper;
using DeployAWS.Application;
using DeployAWS.Application.Dtos;
using DeployAWS.Domain.Core.Interfaces.Services;
using DeployAWS.Domain.Entitys;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DeployAWS.Tests
{
    [TestFixture]
    public class ApplicationServiceClientTests
    {
        private static Fixture _fixture;
        private Mock<IServiceClient> _serviceClientMock;
        private Mock<IMapper> _mapperMock;
        
        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _serviceClientMock = new Mock<IServiceClient>();
            _mapperMock = new Mock<IMapper>();
        }

        [Test]
        public void ApplicationServiceClient_GetAll_ShouldReturnFiveClients()
        {
            //Arrange
            var clients = _fixture.Build<Client>().CreateMany(5);
            var clientsDto = _fixture.Build<ClientDto>().CreateMany(5);

            _serviceClientMock.Setup(x => x.GetAllAsync().Result).Returns(clients);
            _mapperMock.Setup(x => x.Map<IEnumerable<ClientDto>>(clients)).Returns(clientsDto);

            var applicationServiceClient = new ApplicationServiceClient(_serviceClientMock.Object, _mapperMock.Object);

            //Act
            var response = applicationServiceClient.GetAllAsync();

            //Assert
            Assert.IsNotNull(response.Result);
            Assert.AreEqual(5, response.Result.Count());
            _serviceClientMock.VerifyAll();
            _mapperMock.VerifyAll();
        }
        
        [Test]
        public void ApplicationServiceClient_GetById_ShouldReturnClient()
        {
            //Arrange
            const int IdMock = 10;

            var client = _fixture.Build<Client>()
                .With(c => c.Id, IdMock)
                .With(c => c.Email, "teste1@teste.com.br")
                .Create();

            var clientDto = _fixture.Build<ClientDto>()
                .With(c => c.Id, IdMock)
                .With(c => c.Email, "teste1@teste.com.br")
                .Create();

            _serviceClientMock.Setup(x => x.GetByIdAsync(IdMock).Result).Returns(client);
            _mapperMock.Setup(x => x.Map<ClientDto>(client)).Returns(clientDto);

            var applicationServiceClient = new ApplicationServiceClient(_serviceClientMock.Object, _mapperMock.Object);

            //Act
            var response = applicationServiceClient.GetByIdAsync(IdMock);

            //Assert
            Assert.IsNotNull(response.Result);
            Assert.AreEqual("teste1@teste.com.br", response.Result.Email);
            Assert.AreEqual(10, response.Result.Id);
            _serviceClientMock.VerifyAll();
            _mapperMock.VerifyAll();
        }
    }
}