using AutoMapper;
using DeployAWS.Application.Dtos;
using DeployAWS.Application.Interfaces;
using DeployAWS.Domain.Core.Interfaces.Services;
using DeployAWS.Domain.Entitys;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeployAWS.Application
{
    public class ApplicationServiceProduct : IApplicationServiceProduct
    {
        private readonly IServiceProduct _serviceProduct;
        private readonly IMapper _mapper;
        private readonly ILogger<ApplicationServiceProduct> _logger;

        public ApplicationServiceProduct(IServiceProduct serviceProduct
            , IMapper mapper
            , ILogger<ApplicationServiceProduct> logger)
        {
            _serviceProduct = serviceProduct;
            _mapper = mapper;
            _logger = logger;
        }

        public void CreateAsync(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            _logger.LogInformation($"##### Executando request CreateAsync => ApplicationServiceProduct id: {productDto.Id} #####");
            _serviceProduct.CreateAsync(product);
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            _logger.LogInformation($"##### Executando request GetAllAsync => ApplicationServiceProduct #####");
            var products = await _serviceProduct.GetAllAsync();
            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);

            return productsDto;
        }

        public async Task<ProductDto> GetByIdAsync(string id)
        {
            _logger.LogInformation($"##### Executando request GetByIdAsync => ApplicationServiceProduct id: {id} #####");
            var product = await _serviceProduct.GetByIdAsync(id);
            var productDto = _mapper.Map<ProductDto>(product);

            return productDto;
        }

        public bool Remove(string id)
        {
            try
            {
                _logger.LogInformation($"##### Executando request GetByIdAsync => ApplicationServiceProduct id: {id} #####");
                var product = _serviceProduct.GetByIdAsync(id);

                if (product.Result == null)
                    return false;

                _logger.LogInformation($"##### Executando request Remove => ApplicationServiceProduct id: {id} #####");
                var result = _serviceProduct.Remove(id);
                return result;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Update(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            _logger.LogInformation($"##### Executando request Update => ApplicationServiceProduct id: {productDto.Id} #####");
            _serviceProduct.Update(product);
        }
    }
}