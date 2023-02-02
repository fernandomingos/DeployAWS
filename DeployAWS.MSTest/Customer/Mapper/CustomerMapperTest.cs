using AutoMapper;
using DeployAWS.Application.Mappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DeployAWS.MSTest.Customer.Mapper
{
    [TestClass]
    public class CustomerMapperTest
    {
        [TestMethod]
        public void AutoMapperDtoToModelCustomer_Configuration_IsValid()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<DtoToModelMappingCustomer>());
            config.AssertConfigurationIsValid();
        }

        [TestMethod]
        public void AutoMapperModelToDtoCustomer_Configuration_IsValid()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<ModelToDtoMappingCustomer>());
            config.AssertConfigurationIsValid();
        }
    }
}
