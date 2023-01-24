using DeployAWS.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeployAWS.Application.Interfaces
{
    public interface IApplicationServiceCliente
    {
        void Add(ClienteDto clienteDto);

        void Update(ClienteDto clienteDto);

        bool Remove(int id);

        Task<IEnumerable<ClienteDto>> GetAllAsync();

        Task<ClienteDto> GetByIdAsync(int id);
    }
}