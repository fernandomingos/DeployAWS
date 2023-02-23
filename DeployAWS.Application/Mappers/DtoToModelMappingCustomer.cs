using AutoMapper;
using DeployAWS.Application.Dtos;
using DeployAWS.Domain.Entitys;

namespace DeployAWS.Application.Mappers
{
    public class DtoToModelMappingCustomer : Profile
    {
        public DtoToModelMappingCustomer()
        {
            CustomerMap();
        }

        private void CustomerMap()
        {
            CreateMap<CustomerDto, Customer>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(x => x.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(x => x.LastName))
                .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(x => x.EmailAddress))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(x => x.UserName))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(x => x.Password))
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(x => x.CreateDate))
                .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(x => x.ModifiedDate))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(x => x.IsActive));
        }
    }
}
