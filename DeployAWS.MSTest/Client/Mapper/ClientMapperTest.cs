using AutoMapper;
using DeployAWS.Application.Mappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DeployAWS.MSTest.Client.Mapper
{
    [TestClass]
    public class ClientMapperTest
    {
        [TestMethod]
        public void AutoMapperDtoToModelClient_Configuration_IsValid()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<DtoToModelMappingClient>());
            config.AssertConfigurationIsValid();
        }

        [TestMethod]
        public void AutoMapperModelToDtoClient_Configuration_IsValid()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<ModelToDtoMappingClient>());
            config.AssertConfigurationIsValid();
        }
    }
}
