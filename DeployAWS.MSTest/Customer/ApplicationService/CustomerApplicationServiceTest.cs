using AutoFixture;
using AutoMapper;
using DeployAWS.Application;
using DeployAWS.Application.Dtos;
using DeployAWS.Domain.Core.Interfaces.Services;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace DeployAWS.MSTest.Customer.ApplicationService
{
    public class CustomerApplicationServiceTest
    {
        private static Fixture _fixture;
        private readonly Mock<IServiceCustomer> _serviceCustomerMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ILogger<ApplicationServiceCustomer>> _logger;

        public CustomerApplicationServiceTest()
        {
            _fixture = new Fixture();
            _serviceCustomerMock = new Mock<IServiceCustomer>();
            _mapperMock = new Mock<IMapper>();
            _logger = new Mock<ILogger<ApplicationServiceCustomer>>();
        }

        [TestMethod]
        public void ApplicationServiceCustomer_GetAll_ShouldReturnFiveCustomers()
        {
            //Arrange
            var Customers = _fixture.Build<Domain.Entitys.Customer>().CreateMany(5);
            var CustomersDto = _fixture.Build<CustomerDto>().CreateMany(5);
            _serviceCustomerMock.Setup(x => x.GetAllAsync().Result).Returns(Customers);
            _mapperMock.Setup(x => x.Map<IEnumerable<CustomerDto>>(Customers)).Returns(CustomersDto);
            var applicationServiceCustomer = new ApplicationServiceCustomer(_serviceCustomerMock.Object, _mapperMock.Object, _logger.Object);

            //Act
            var response = applicationServiceCustomer.GetAllAsync();

            //Assert
            response.Result.Should().NotBeNullOrEmpty();
            response.Result.Should().HaveCount(5);
            _serviceCustomerMock.VerifyAll();
            _mapperMock.VerifyAll();
        }

        [TestMethod]
        public void ApplicationServiceCustomer_GetById_ShouldReturnCustomer()
        {
            //Arrange
            string IdMock = Guid.NewGuid().ToString();
            const string EmailTest = "teste1@teste.com.br";

            var Customer = _fixture.Build<Domain.Entitys.Customer>()
                .With(c => c.Id, IdMock)
                .With(c => c.EmailAddress, EmailTest)
                .Create();

            var CustomerDto = _fixture.Build<CustomerDto>()
                .With(c => c.Id, IdMock)
                .With(c => c.EmailAddress, EmailTest)
                .Create();

            _serviceCustomerMock.Setup(x => x.GetByIdAsync(IdMock).Result).Returns(Customer);
            _mapperMock.Setup(x => x.Map<CustomerDto>(Customer)).Returns(CustomerDto);

            var applicationServiceCustomer = new ApplicationServiceCustomer(_serviceCustomerMock.Object, _mapperMock.Object, _logger.Object);

            //Act
            var response = applicationServiceCustomer.GetByIdAsync(IdMock);

            //Assert
            response.Result.Should().NotBeNull();
            response.Result.EmailAddress.Should().Be(EmailTest);
            response.Result.Id.Should().Be(IdMock);
            _serviceCustomerMock.VerifyAll();
            _mapperMock.VerifyAll();
        }
    }
}
