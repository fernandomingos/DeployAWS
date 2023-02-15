using AutoMapper;
using DeployAWS.Application.Dtos;
using DeployAWS.Application.Interfaces;
using DeployAWS.Domain.Core.Interfaces.Services;
using DeployAWS.Domain.Entitys;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeployAWS.Application
{
    public class ApplicationServiceCustomer : IApplicationServiceCustomer
    {
        private readonly IServiceCustomer _serviceCustomer;
        private readonly IMapper _mapper;
        public ApplicationServiceCustomer(IServiceCustomer serviceCustomer, IMapper mapper)
        {
            _serviceCustomer = serviceCustomer;
            _mapper = mapper;
        }

        public Customer Add(CustomerDto customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);
            _serviceCustomer.Add(customer);
            return customer;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            var customers = await _serviceCustomer.GetAllAsync();
            var customerDto = _mapper.Map<IEnumerable<CustomerDto>>(customers);

            return customerDto;
        }

        public async Task<CustomerDto> GetByIdAsync(string id)
        {
            var customer = await _serviceCustomer.GetByIdAsync(id);
            var customerDto = _mapper.Map<CustomerDto>(customer);

            return customerDto;
        }

        public bool Remove(string id)
        {
            var customer = _serviceCustomer.GetByIdAsync(id);

            if (customer.Result == null)
                return false;

            _serviceCustomer.Remove(id);

            return true;
        }

        public void Update(CustomerDto customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);
            _serviceCustomer.Update(customer);
        }
    }
}
