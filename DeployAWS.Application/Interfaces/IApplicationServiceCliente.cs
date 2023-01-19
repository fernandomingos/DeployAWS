using DeployAWS.Application.Dtos;
using System.Collections.Generic;

namespace DeployAWS.Application.Interfaces
{
    public interface IApplicationServiceCliente
    {
        void Add(ClienteDto clienteDto);

        void Update(ClienteDto clienteDto);

        bool Remove(int id);

        IEnumerable<ClienteDto> GetAll();

        ClienteDto GetById(int id);
    }
}