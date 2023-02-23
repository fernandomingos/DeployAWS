using AutoMapper;
using DeployAWS.Application.Dtos;
using DeployAWS.Domain.Entitys;

namespace DeployAWS.Application.Mappers
{
    public class ModelToDtoMappingOrder : Profile
    {
        public ModelToDtoMappingOrder()
        {
            OrderDtoMap();
        }

        private void OrderDtoMap()
        {
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(x => x.Status))
                //.ForMember(dest => dest.Customer, opt => opt.MapFrom(x => x.Customer))
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(x => x.CreateDate))
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(x => x.ModifiedDate))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(x => x.Items));
        }
    }
}
