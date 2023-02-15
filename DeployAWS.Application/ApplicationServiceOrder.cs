using AutoMapper;
using DeployAWS.Application.Dtos;
using DeployAWS.Application.Interfaces;
using DeployAWS.Domain.Core.Interfaces.Services;
using DeployAWS.Domain.Entitys;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeployAWS.Application
{
    public class ApplicationServiceOrder : IApplicationServiceOrder
    {
        private readonly IServiceOrder _serviceOrder;
        private readonly IMapper _mapper;

        public ApplicationServiceOrder(IServiceOrder serviceOrder
                                        , IMapper mapper)
        {
            _serviceOrder = serviceOrder;
            _mapper = mapper;
        }
        public Order Add(OrderDto orderDto)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrderDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<OrderDto> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public bool Remove(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(OrderDto orderDto)
        {
            throw new NotImplementedException();
        }
    }
}
