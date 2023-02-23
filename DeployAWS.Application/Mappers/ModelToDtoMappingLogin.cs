using AutoMapper;
using DeployAWS.Application.Dtos;
using DeployAWS.Domain.Entitys;

namespace DeployAWS.Application.Mappers
{
    public class ModelToDtoMappingLogin : Profile
    {
        public ModelToDtoMappingLogin()
        {
            LoginMap();
        }

        private void LoginMap()
        {
            CreateMap<Login, LoginDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(x => x.UserName))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(x => x.Password));
        }
    }
}
