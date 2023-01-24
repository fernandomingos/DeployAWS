using AutoMapper;
using DeployAWS.Application.Dtos;
using DeployAWS.Application.Interfaces;
using DeployAWS.Domain.Core.Interfaces.Services;
using DeployAWS.Domain.Entitys;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeployAWS.Application
{
    public class ApplicationServiceCliente : IApplicationServiceCliente
    {
        private readonly IServiceCliente _serviceCliente;
        private readonly IMapper _mapper;
        public ApplicationServiceCliente(IServiceCliente serviceCliente, IMapper mapper)
        {
            _serviceCliente = serviceCliente;
            _mapper = mapper;
        }

        public void Add(ClienteDto clienteDto)
        {
            var cliente = _mapper.Map<Cliente>(clienteDto);
            _serviceCliente.Add(cliente);
        }

        public async Task<IEnumerable<ClienteDto>> GetAllAsync()
        {
            var clientes = await _serviceCliente.GetAllAsync();
            var clientesDto = _mapper.Map<IEnumerable<ClienteDto>>(clientes);

            return clientesDto;
        }

        public async Task<ClienteDto> GetByIdAsync(int id)
        {
            var cliente = await _serviceCliente.GetByIdAsync(id);
            var clienteDto = _mapper.Map<ClienteDto>(cliente);

            return clienteDto;
        }

        public bool Remove(int id)
        {
            var cliente = _serviceCliente.GetByIdAsync(id);

            if (cliente == null)
                return false;

            _serviceCliente.Remove(id);

            return true;
        }

        public void Update(ClienteDto clienteDto)
        {
            var cliente = _mapper.Map<Cliente>(clienteDto);
            _serviceCliente.Update(cliente);
        }
    }
}
