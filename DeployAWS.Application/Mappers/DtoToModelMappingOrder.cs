using AutoMapper;
using DeployAWS.Application.Dtos;
using DeployAWS.Domain.Entitys;

namespace DeployAWS.Application.Mappers
{
    public class DtoToModelMappingOrder : Profile
    {
        public DtoToModelMappingOrder()
        {
            OrderMap();
        }

        private void OrderMap()
        {
            CreateMap<OrderDto, Order>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(x => x.Status))
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(x => x.CreateDate))
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(x => x.ModifiedDate))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(x => x.Items));
        }
    }
}
