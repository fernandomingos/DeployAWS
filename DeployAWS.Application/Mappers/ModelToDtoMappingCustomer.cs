﻿using AutoMapper;
using DeployAWS.Application.Dtos;
using DeployAWS.Domain.Entitys;

namespace DeployAWS.Application.Mappers
{
    public class ModelToDtoMappingCustomer : Profile
    {

        public ModelToDtoMappingCustomer()
        {
            CustomerDtoMap();
        }

        private void CustomerDtoMap()
        {
            CreateMap<Customer, CustomerDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(x => x.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(x => x.LastName))
                .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(x => x.EmailAddress))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(x => x.UserName))
                .ForMember(dest => dest.Profile, opt => opt.MapFrom(x => x.Profile))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(x => x.IsActive))
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(x => x.CreateDate));

        }
    }
}
