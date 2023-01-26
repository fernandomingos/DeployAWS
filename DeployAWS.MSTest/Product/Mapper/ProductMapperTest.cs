using AutoMapper;
using DeployAWS.Application.Mappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DeployAWS.MSTest.Product.Mapper
{
    [TestClass]
    public class ProductMapperTest
    {
        [TestMethod]
        public void AutoMapperDtoToModelProduct_Configuration_IsValid()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<DtoToModelMappingProduct>());
            config.AssertConfigurationIsValid();
        }

        [TestMethod]
        public void AutoMapperModelToDtoProduct_Configuration_IsValid()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<ModelToDtoMappingProduct>());
            config.AssertConfigurationIsValid();
        }
    }
}
