using DeployAWS.Application.Dtos;
using DeployAWS.Domain.Entitys;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeployAWS.Application.Interfaces
{
    public interface IApplicationServiceCustomer
    {
        Customer Add(CustomerDto customerDto);
        void Update(CustomerDto customerDto);
        bool Remove(String id);
        Task<IEnumerable<CustomerDto>> GetAllAsync();
        Task<CustomerDto> GetByIdAsync(String id);
        Task<CustomerDto> PostLoginAsync(LoginDto login);
    }
}