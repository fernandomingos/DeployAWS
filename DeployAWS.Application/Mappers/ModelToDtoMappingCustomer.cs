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
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(x => x.Nome))
                .ForMember(dest => dest.Sobrenome, opt => opt.MapFrom(x => x.Sobrenome))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(x => x.Email));
        }
    }
}