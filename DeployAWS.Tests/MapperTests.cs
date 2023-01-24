using AutoMapper;
using NUnit.Framework;
using DeployAWS.Application.Mappers;

namespace DeployAWS.Tests
{
    [TestFixture]
    public class MapperTests
    {
        [Test]
        public void AutoMapperDtoToModelClient_Configuration_IsValid()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<DtoToModelMappingClient>());
            config.AssertConfigurationIsValid();
        }  
        
        [Test]
        public void AutoMapperModelToDtoClient_Configuration_IsValid()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<ModelToDtoMappingClient>());
            config.AssertConfigurationIsValid();
        }
        
        [Test]
        public void AutoMapperDtoToModelProduct_Configuration_IsValid()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<DtoToModelMappingProduct>());
            config.AssertConfigurationIsValid();
        }
        
        [Test]
        public void AutoMapperModelToDtoProduct_Configuration_IsValid()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<ModelToDtoMappingProduct>());
            config.AssertConfigurationIsValid();
        }  

    }
}