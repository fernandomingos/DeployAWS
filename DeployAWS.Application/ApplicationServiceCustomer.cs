using AutoMapper;
using DeployAWS.Application.Dtos;
using DeployAWS.Application.Interfaces;
using DeployAWS.Domain.Core.Interfaces.Services;
using DeployAWS.Domain.Entitys;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeployAWS.Application
{
    public class ApplicationServiceCustomer : IApplicationServiceCustomer
    {
        private readonly IServiceCustomer _serviceCustomer;
        private readonly IMapper _mapper;
        private readonly ILogger<ApplicationServiceCustomer> _logger;
        public ApplicationServiceCustomer(IServiceCustomer serviceCustomer, IMapper mapper
            , ILogger<ApplicationServiceCustomer> logger)
        {
            _serviceCustomer = serviceCustomer;
            _mapper = mapper;
            _logger = logger;
        }

        public Customer Add(CustomerDto customerDto)
        {
            _logger.LogInformation($"##### Executando API Add => ApplicationServiceCustomer #####");
            var customer = _mapper.Map<Customer>(customerDto);
            _serviceCustomer.Add(customer);

            return customer;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            _logger.LogInformation($"##### Executando método GetAllAsync => ApplicationServiceCustomer #####");
            var customers = await _serviceCustomer.GetAllAsync();
            var customerDto = _mapper.Map<IEnumerable<CustomerDto>>(customers);

            return customerDto;
        }

        public async Task<CustomerDto> GetByIdAsync(string id)
        {
            _logger.LogInformation($"##### Executando método GetByIdAsync => ApplicationServiceCustomer id: {id} #####");
            var customer = await _serviceCustomer.GetByIdAsync(id);
            var customerDto = _mapper.Map<CustomerDto>(customer);

            return customerDto;
        }

        public bool Remove(string id)
        {
            _logger.LogInformation($"##### Executando método Remove => ApplicationServiceCustomer id: {id} #####");
            var customer = _serviceCustomer.GetByIdAsync(id);

            if (customer.Result == null)
                return false;

            _serviceCustomer.Remove(id);

            return true;
        }

        public void Update(CustomerDto customerDto)
        {
            _logger.LogInformation($"##### Executando método Update => ApplicationServiceCustomer id: {customerDto.Id} #####");
            var customer = _mapper.Map<Customer>(customerDto);
            _serviceCustomer.Update(customer);
        }
    }
}
