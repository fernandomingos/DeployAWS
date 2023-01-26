using AutoFixture;
using DeployAWS.Application.Dtos;
using DeployAWS.Application.Validator;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DeployAWS.MSTest.Product.Validation
{
    [TestClass]
    public class ProductValidationTest
    {
        private readonly ProductDtoValidator _productDtoValidator;
        private static Fixture _fixture;

        public ProductValidationTest()
        {
            _fixture = new Fixture();
            _productDtoValidator = new ProductDtoValidator();
        }

        [TestMethod]
        public void Test_Product_Validator_OK()
        {
            var productDto = _fixture.Build<ProductDto>().Create();
            var validation = _productDtoValidator.Validate(productDto);
            validation.Errors.Should().BeNullOrEmpty();
        }
        [TestMethod]
        public void Test_Product_Name_Empty()
        {
            var productDto = _fixture.Build<ProductDto>()
                .With(c => c.Nome, string.Empty)
                .Create();
            var validation = _productDtoValidator.Validate(productDto);
            validation.Errors.Should().NotBeNullOrEmpty();
        }
        [TestMethod]
        public void Test_Product_Validator_Name_Exceeded_MaxLength()
        {
            var productDto = _fixture.Build<ProductDto>()
                .With(n => n.Nome, "012345678901234567890123456789012345678901234567890123456789012345678901234567890")
                .Create();
            var validation = _productDtoValidator.Validate(productDto);
            validation.Errors.Should().NotBeNullOrEmpty();
        }
    }
}
