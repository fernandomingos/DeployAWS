using DeployAWS.Application.Dtos;
using DeployAWS.Domain.Entitys;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeployAWS.Application.Interfaces
{
    public interface IApplicationServiceCustomer
    {
        Customer Add(CustomerDto customerDto);

        void Update(CustomerDto customerDto);

        bool Remove(string id);

        Task<IEnumerable<CustomerDto>> GetAllAsync();

        Task<CustomerDto> GetByIdAsync(string id);
    }
}