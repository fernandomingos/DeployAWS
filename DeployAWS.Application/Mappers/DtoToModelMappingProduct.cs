using AutoMapper;
using DeployAWS.Application.Dtos;
using DeployAWS.Domain.Entitys;

namespace DeployAWS.Application.Mappers
{
    public class DtoToModelMappingProduct : Profile
    {
        public DtoToModelMappingProduct()
        {
            ProductMap();
        }

        private void ProductMap()
        {
            CreateMap<ProductDto, Product>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(x => x.Value))
                .ForMember(dest => dest.IsAvaiable, opt => opt.Ignore());
        }
    }
}
