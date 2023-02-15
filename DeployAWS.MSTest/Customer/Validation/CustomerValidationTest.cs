using AutoFixture;
using DeployAWS.Application.Dtos;
using DeployAWS.Application.Validator;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DeployAWS.MSTest.Customer.Validation
{
    [TestClass]
    public class CustomerValidationTest
    {
        private readonly CustomerDtoValidator _customerDtoValidator;
        private static Fixture _fixture;
        CustomerDto customerDto;

        public CustomerValidationTest()
        {
            _fixture= new Fixture();
            _customerDtoValidator = new CustomerDtoValidator();

            customerDto = _fixture.Build<CustomerDto>()
                .With(c => c.FirstName, string.Empty)
                .With(c => c.LastName, string.Empty)
                .With(c => c.EmailAddress, string.Empty)
                .Create();
        }

        [TestMethod]
        public void Test_Customer_Validator_OK()
        {
            var customerDtoOk = _fixture.Build<CustomerDto>().Create();
            var validation = _customerDtoValidator.Validate(customerDtoOk);
            validation.Errors.Should().BeNullOrEmpty();
        }

        [TestMethod]
        public void Test_Customer_Validator_Name_Empty()
        {
            var validation = _customerDtoValidator.Validate(customerDto);
            validation.Errors.Should().NotBeNullOrEmpty();
        }
        [TestMethod]
        public void Test_Customer_Validator_Name_Exceeded_MaxLength()
        {
            var customerDto = _fixture.Build<CustomerDto>().Create();
            customerDto.FirstName = "012345678901234567890123456789012345678901234567890123456789012345678901234567890";
            var validation = _customerDtoValidator.Validate(customerDto);
            validation.Errors.Should().NotBeNullOrEmpty();
        }
        [TestMethod]
        public void Test_Customer_Validator_LastName_Empty()
        {
            var validation = _customerDtoValidator.Validate(customerDto);
            validation.Errors.Should().NotBeNullOrEmpty();
        }
        public void Test_Customer_Validator_LastName_Exceeded_MaxLength()
        {
            var customerDto = _fixture.Build<CustomerDto>().Create();
            customerDto.LastName = "012345678901234567890123456789012345678901234567890123456789012345678901234567890";
            var validation = _customerDtoValidator.Validate(customerDto);
            validation.Errors.Should().NotBeNullOrEmpty();
        }
        [TestMethod]
        public void Test_Customer_Validator_Email_Empty()
        {
            var validation = _customerDtoValidator.Validate(customerDto);
            validation.Errors.Should().NotBeNullOrEmpty();
        }
    }
}
