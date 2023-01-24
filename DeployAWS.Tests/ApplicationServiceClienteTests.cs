using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using AutoMapper;
using Moq;
using NUnit.Framework;
using DeployAWS.Application;
using DeployAWS.Application.Dtos;
using DeployAWS.Domain.Core.Interfaces.Services;
using DeployAWS.Domain.Entitys;
using System.Threading.Tasks;

namespace DeployAWS.Tests
{
    [TestFixture]
    public class ApplicationServiceClienteTests
    {

        private static Fixture _fixture;
        private Mock<IServiceCliente> _serviceClienteMock;
        private Mock<IMapper> _mapperMock;
        

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _serviceClienteMock = new Mock<IServiceCliente>();
            _mapperMock = new Mock<IMapper>();
        }

        [Test]
        public void ApplicationServiceCliente_GetAll_ShouldReturnFiveClients()
        {
            //Arrange
            var clientes = _fixture.Build<Cliente>().CreateMany(5);
            var clientesDto = _fixture.Build<ClienteDto>().CreateMany(5);

            _serviceClienteMock.Setup(x => x.GetAllAsync().Result).Returns(clientes);
            _mapperMock.Setup(x => x.Map<IEnumerable<ClienteDto>>(clientes)).Returns(clientesDto);

            var applicationServiceCliente = new ApplicationServiceCliente(_serviceClienteMock.Object, _mapperMock.Object);

            //Act
            var response = applicationServiceCliente.GetAllAsync();

            //Assert
            Assert.IsNotNull(response.Result);
            Assert.AreEqual(5, response.Result.Count());
            _serviceClienteMock.VerifyAll();
            _mapperMock.VerifyAll();
        }
        
        [Test]
        public void ApplicationServiceCliente_GetById_ShouldReturnClient()
        {
            //Arrange
            const int id = 10;

            var cliente = _fixture.Build<Cliente>()
                .With(c => c.Id, id)
                .With(c => c.Email, "teste1@teste.com.br")
                .Create();

            var clienteDto = _fixture.Build<ClienteDto>()
                .With(c => c.Id, id)
                .With(c => c.Email, "teste1@teste.com.br")
                .Create();

            _serviceClienteMock.Setup(x => x.GetByIdAsync(id).Result).Returns(cliente);
            _mapperMock.Setup(x => x.Map<ClienteDto>(cliente)).Returns(clienteDto);

            var applicationServiceCliente =
                new ApplicationServiceCliente(_serviceClienteMock.Object, _mapperMock.Object);

            //Act
            var response = applicationServiceCliente.GetByIdAsync(id);

            //Assert
            Assert.IsNotNull(response.Result);
            Assert.AreEqual("teste1@teste.com.br", response.Result.Email);
            Assert.AreEqual(10, response.Result.Id);
            _serviceClienteMock.VerifyAll();
            _mapperMock.VerifyAll();
        }
    }
}