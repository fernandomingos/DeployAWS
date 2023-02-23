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
            _logger.LogInformation($"##### Executando request GetAllAsync => ApplicationServiceCustomer #####");
            var customers = await _serviceCustomer.GetAllAsync();
            var customerDto = _mapper.Map<IEnumerable<CustomerDto>>(customers);

            return customerDto;
        }

        public async Task<CustomerDto> GetByIdAsync(string id)
        {
            _logger.LogInformation($"##### Executando request GetByIdAsync => ApplicationServiceCustomer id: {id} #####");
            var customer = await _serviceCustomer.GetByIdAsync(id);
            var customerDto = _mapper.Map<CustomerDto>(customer);

            return customerDto;
        }

        public bool Remove(string id)
        {
            _logger.LogInformation($"##### Executando request Remove => ApplicationServiceCustomer id: {id} #####");
            var customer = _serviceCustomer.GetByIdAsync(id);

            if (customer.Result == null)
            {
                _logger.LogInformation($"##### Executando request Remove => ApplicationServiceCustomer id: {id} não encontrado #####");
                return false;
            }

            _serviceCustomer.Remove(id);

            return true;
        }

        public void Update(CustomerDto customerDto)
        {
            _logger.LogInformation($"##### Executando request Update => ApplicationServiceCustomer id: {customerDto.Id} #####");
            var customer = _mapper.Map<Customer>(customerDto);
            _serviceCustomer.Update(customer);
        }

        public async Task<CustomerDto> LoginAsync(LoginDto loginDto)
        {
            _logger.LogInformation($"##### Executando request PostLoginAsync => ApplicationServiceCustomer username: {loginDto.UserName} #####");
            var login = _mapper.Map<Login>(loginDto);
            var customer = await _serviceCustomer.PostLoginAsync(login);
            var customerDto = _mapper.Map<CustomerDto>(customer);

            return customerDto;
        }
    }
}
