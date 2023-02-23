using AutoMapper;
using DeployAWS.Application.Dtos;
using DeployAWS.Domain.Entitys;

namespace DeployAWS.Application.Mappers
{
    public class ModelToDtoMappingProduct : Profile
    {
        public ModelToDtoMappingProduct()
        {
            ProductDtoMap();
        }
        private void ProductDtoMap()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(x => x.Value))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(x => x.Amount))
                .ForMember(dest => dest.IsAvaiable, opt => opt.MapFrom(x => x.IsAvaiable));
        }
    }
}
