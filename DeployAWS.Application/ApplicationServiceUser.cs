using AutoMapper;
using DeployAWS.Application.Dtos;
using DeployAWS.Application.Interfaces;
using DeployAWS.Domain.Core.Interfaces.Services;
using System.Threading.Tasks;

namespace DeployAWS.Application
{
    public class ApplicationServiceUser : IApplicationServiceUser
    {
        private readonly IServiceCustomer _serviceCustomer;
        private readonly IMapper _mapper;

        public ApplicationServiceUser(IServiceCustomer serviceCustomer
                                        , IMapper mapper)
        {
            _serviceCustomer = serviceCustomer;
            _mapper = mapper;
        }

        public async Task<CustomerDto> GetByIdAsync(AuthenticationDto AuthenticationDto)
        {
            var customerDto = await _serviceCustomer.GetByIdAsync(AuthenticationDto.Id);
            return _mapper.Map<CustomerDto>(customerDto);             
        }
    }
}
