﻿using DeployAWS.Application.Dtos;
using DeployAWS.Domain.Entitys;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeployAWS.Application.Interfaces
{
    public interface IApplicationServiceOrder
    {
        Order Add(OrderDto orderDto);

        void Update(OrderDto orderDto);

        bool Remove(string id);

        Task<IEnumerable<OrderDto>> GetAllAsync();

        Task<OrderDto> GetByIdAsync(string id);
    }
}