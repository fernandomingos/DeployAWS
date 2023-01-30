using DeployAWS.Application.Dtos;
using DeployAWS.Domain.Entitys;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeployAWS.Application.Interfaces
{
    public interface IApplicationServiceClient
    {
        Client Add(ClientDto clieneDto);

        void Update(ClientDto clientDto);

        bool Remove(int id);

        Task<IEnumerable<ClientDto>> GetAllAsync();

        Task<ClientDto> GetByIdAsync(int id);
    }
}