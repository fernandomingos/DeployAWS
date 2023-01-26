using AutoFixture;
using AutoMapper;
using DeployAWS.Application;
using DeployAWS.Application.Dtos;
using DeployAWS.Domain.Core.Interfaces.Services;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace DeployAWS.MSTest.Client.ApplicationService
{
    public class ClientApplicationServiceTest
    {
        private static Fixture _fixture;
        private Mock<IServiceClient> _serviceClientMock;
        private Mock<IMapper> _mapperMock;

        public ClientApplicationServiceTest()
        {
            _fixture = new Fixture();
            _serviceClientMock = new Mock<IServiceClient>();
            _mapperMock = new Mock<IMapper>();
        }

        [TestMethod]
        public void ApplicationServiceClient_GetAll_ShouldReturnFiveClients()
        {
            //Arrange
            var clients = _fixture.Build<Domain.Entitys.Client>().CreateMany(5);
            var clientsDto = _fixture.Build<ClientDto>().CreateMany(5);
            _serviceClientMock.Setup(x => x.GetAllAsync().Result).Returns(clients);
            _mapperMock.Setup(x => x.Map<IEnumerable<ClientDto>>(clients)).Returns(clientsDto);
            var applicationServiceClient = new ApplicationServiceClient(_serviceClientMock.Object, _mapperMock.Object);

            //Act
            var response = applicationServiceClient.GetAllAsync();

            //Assert
            response.Result.Should().NotBeNullOrEmpty();
            response.Result.Should().HaveCount(5);
            _serviceClientMock.VerifyAll();
            _mapperMock.VerifyAll();
        }

        [TestMethod]
        public void ApplicationServiceClient_GetById_ShouldReturnClient()
        {
            //Arrange
            const int IdMock = 10;
            const string EmailTest = "teste1@teste.com.br";

            var client = _fixture.Build<Domain.Entitys.Client>()
                .With(c => c.Id, IdMock)
                .With(c => c.Email, EmailTest)
                .Create();

            var clientDto = _fixture.Build<ClientDto>()
                .With(c => c.Id, IdMock)
                .With(c => c.Email, EmailTest)
                .Create();

            _serviceClientMock.Setup(x => x.GetByIdAsync(IdMock).Result).Returns(client);
            _mapperMock.Setup(x => x.Map<ClientDto>(client)).Returns(clientDto);

            var applicationServiceClient = new ApplicationServiceClient(_serviceClientMock.Object, _mapperMock.Object);

            //Act
            var response = applicationServiceClient.GetByIdAsync(IdMock);

            //Assert
            response.Result.Should().NotBeNull();
            response.Result.Email.Should().Be(EmailTest);
            response.Result.Id.Should().Be(IdMock);
            _serviceClientMock.VerifyAll();
            _mapperMock.VerifyAll();
        }
    }
}
