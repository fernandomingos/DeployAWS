using AutoFixture;
using DeployAWS.Application.Dtos;
using DeployAWS.Application.Validator;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DeployAWS.MSTest.Client.Validation
{
    [TestClass]
    public class ClientValidationTest
    {
        private readonly ClientDtoValidator _clientDtoValidator;
        private static Fixture _fixture;
        ClientDto clientDto;

        public ClientValidationTest()
        {
            _fixture= new Fixture();
            _clientDtoValidator = new ClientDtoValidator();

            clientDto = _fixture.Build<ClientDto>()
                .With(c => c.Nome, string.Empty)
                .With(c => c.Sobrenome, string.Empty)
                .With(c => c.Email, string.Empty)
                .Create();
        }

        [TestMethod]
        public void Test_Client_Validator_OK()
        {
            var clientDtoOk = _fixture.Build<ClientDto>().Create();
            var validation = _clientDtoValidator.Validate(clientDtoOk);
            validation.Errors.Should().BeNullOrEmpty();
        }

        [TestMethod]
        public void Test_Client_Validator_Name_Empty()
        {
            var validation = _clientDtoValidator.Validate(clientDto);
            validation.Errors.Should().NotBeNullOrEmpty();
        }
        [TestMethod]
        public void Test_Client_Validator_Name_Exceeded_MaxLength()
        {
            var clientDto = _fixture.Build<ClientDto>().Create();
            clientDto.Nome = "012345678901234567890123456789012345678901234567890123456789012345678901234567890";
            var validation = _clientDtoValidator.Validate(clientDto);
            validation.Errors.Should().NotBeNullOrEmpty();
        }
        [TestMethod]
        public void Test_Client_Validator_LastName_Empty()
        {
            var validation = _clientDtoValidator.Validate(clientDto);
            validation.Errors.Should().NotBeNullOrEmpty();
        }
        public void Test_Client_Validator_LastName_Exceeded_MaxLength()
        {
            var clientDto = _fixture.Build<ClientDto>().Create();
            clientDto.Sobrenome = "012345678901234567890123456789012345678901234567890123456789012345678901234567890";
            var validation = _clientDtoValidator.Validate(clientDto);
            validation.Errors.Should().NotBeNullOrEmpty();
        }
        [TestMethod]
        public void Test_Client_Validator_Email_Empty()
        {
            var validation = _clientDtoValidator.Validate(clientDto);
            validation.Errors.Should().NotBeNullOrEmpty();
        }
    }
}
