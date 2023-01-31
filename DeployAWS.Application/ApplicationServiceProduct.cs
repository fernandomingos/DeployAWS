using AutoMapper;
using DeployAWS.Application.Dtos;
using DeployAWS.Application.Interfaces;
using DeployAWS.Domain.Core.Interfaces.Services;
using DeployAWS.Domain.Entitys;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeployAWS.Application
{
    public class ApplicationServiceProduct : IApplicationServiceProduct
    {
        private readonly IServiceProduct _serviceProduct;
        private readonly IMapper _mapper;

        public ApplicationServiceProduct(IServiceProduct serviceProduct
                                        , IMapper mapper)
        {
            _serviceProduct = serviceProduct;
            _mapper = mapper;
        }

        public void CreateAsync(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            _serviceProduct.CreateAsync(product);
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _serviceProduct.GetAllAsync();
            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);

            return productsDto;
        }

        public async Task<ProductDto> GetByIdAsync(string id)
        {
            var product = await _serviceProduct.GetByIdAsync(id);
            var productDto = _mapper.Map<ProductDto>(product);

            return productDto;
        }

        public bool RemoveAsync(string id)
        {
            try
            {
                var product = _serviceProduct.GetByIdAsync(id);

                if (product.Result == null)
                    return false;

                _serviceProduct.RemoveAsync(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void UpdateAsync(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            _serviceProduct.UpdateAsync(product);
        }
    }
}