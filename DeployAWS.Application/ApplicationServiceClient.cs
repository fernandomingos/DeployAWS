using AutoMapper;
using DeployAWS.Application.Dtos;
using DeployAWS.Application.Interfaces;
using DeployAWS.Domain.Core.Interfaces.Services;
using DeployAWS.Domain.Entitys;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeployAWS.Application
{
    public class ApplicationServiceClient : IApplicationServiceClient
    {
        private readonly IServiceClient _serviceClient;
        private readonly IMapper _mapper;
        public ApplicationServiceClient(IServiceClient serviceClient, IMapper mapper)
        {
            _serviceClient = serviceClient;
            _mapper = mapper;
        }

        public void Add(ClientDto clientDto)
        {
            var client = _mapper.Map<Client>(clientDto);
            _serviceClient.Add(client);
        }

        public async Task<IEnumerable<ClientDto>> GetAllAsync()
        {
            var clients = await _serviceClient.GetAllAsync();
            var clientsDto = _mapper.Map<IEnumerable<ClientDto>>(clients);

            return clientsDto;
        }

        public async Task<ClientDto> GetByIdAsync(int id)
        {
            var client = await _serviceClient.GetByIdAsync(id);
            var clientDto = _mapper.Map<ClientDto>(client);

            return clientDto;
        }

        public bool Remove(int id)
        {
            var client = _serviceClient.GetByIdAsync(id);

            if (client == null)
                return false;

            _serviceClient.Remove(id);

            return true;
        }

        public void Update(ClientDto clientDto)
        {
            var client = _mapper.Map<Client>(clientDto);
            _serviceClient.Update(client);
        }
    }
}
