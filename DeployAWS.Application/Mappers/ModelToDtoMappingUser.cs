using AutoMapper;
using DeployAWS.Application.Dtos;
using DeployAWS.Domain.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeployAWS.Application.Mappers
{
    public class ModelToDtoMappingUser : Profile
    {
        public ModelToDtoMappingUser()
        {
            CustomerMap();
        }

        private void CustomerMap()
        {
            CreateMap<UserDto, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(x => x.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(x => x.LastName))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(x => x.UserName))
                .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(x => x.EmailAddress))
                .ForMember(dest => dest.Profile, opt => opt.MapFrom(x => x.Profile))
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(x => x.CreateDate));
        }
    }
}
