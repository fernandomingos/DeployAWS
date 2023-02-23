using AutoMapper;
using DeployAWS.Application.Dtos;
using DeployAWS.Domain.Entitys;

namespace DeployAWS.Application.Mappers
{
    public class DtoToModelMappingLogin : Profile
    {
        public DtoToModelMappingLogin()
        {
            LoginMap();
        }

        private void LoginMap()
        {
            CreateMap<LoginDto, Login>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(x => x.UserName))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(x => x.Password));
        }
    }
}
