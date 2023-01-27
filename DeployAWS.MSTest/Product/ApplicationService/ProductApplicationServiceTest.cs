﻿using AutoFixture;
using AutoMapper;
using DeployAWS.Application;
using DeployAWS.Application.Dtos;
using DeployAWS.Domain.Core.Interfaces.Services;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace DeployAWS.MSTest.Product.ApplicationService
{
    public class ProductApplicationServiceTest
    {
        private static Fixture _fixture;
        private Mock<IServiceProduct> _serviceProductMock;
        private Mock<IMapper> _mapperMock;

        public ProductApplicationServiceTest()
        {
            _fixture = new Fixture();
            _serviceProductMock = new Mock<IServiceProduct>();
            _mapperMock = new Mock<IMapper>();
        }

        [TestMethod]
        public void ApplicationServiceProduct_GetAll_ShouldReturnSevenProducts()
        {
            //Arrange
            var products = _fixture.Build<Domain.Entitys.Product>().CreateMany(7);
            var productsDto = _fixture.Build<ProductDto>().CreateMany(7);
            _serviceProductMock.Setup(x => x.GetAllAsync().Result).Returns(products);
            _mapperMock.Setup(x => x.Map<IEnumerable<ProductDto>>(products)).Returns(productsDto);
            var applicationServiceClient = new ApplicationServiceProduct(_serviceProductMock.Object, _mapperMock.Object);

            //Act
            var response = applicationServiceClient.GetAllAsync();

            //Assert
            response.Result.Should().NotBeNullOrEmpty();
            response.Result.Should().HaveCount(7);
            _serviceProductMock.VerifyAll();
            _mapperMock.VerifyAll();
        }

        [TestMethod]
        public void ApplicationServiceClient_GetById_ShouldReturnClient()
        {
            //Arrange
            const int IdMock = 10;
            const string NameTest = "Nome teste";

            var product = _fixture.Build<Domain.Entitys.Product>()
                .With(c => c.Id, IdMock)
                .With(c => c.Nome, NameTest)
                .With(c => c.Valor, 10000)
                .Create();

            var productDto = _fixture.Build<ProductDto>()
                .With(c => c.Id, IdMock)
                .With(c => c.Nome, NameTest)
                .With(c => c.Valor, 10000)
                .Create();

            _serviceProductMock.Setup(x => x.GetByIdAsync(IdMock).Result).Returns(product);
            _mapperMock.Setup(x => x.Map<ProductDto>(product)).Returns(productDto);

            var applicationServiceClient = new ApplicationServiceProduct(_serviceProductMock.Object, _mapperMock.Object);

            //Act
            var response = applicationServiceClient.GetByIdAsync(IdMock);

            //Assert
            response.Result.Should().NotBeNull();
            response.Result.Nome.Should().Be(NameTest);
            response.Result.Id.Should().Be(IdMock);
            _serviceProductMock.VerifyAll();
            _mapperMock.VerifyAll();
        }
    }
}