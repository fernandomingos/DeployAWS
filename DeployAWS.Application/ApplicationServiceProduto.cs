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
    public class ApplicationServiceProduto : IApplicationServiceProduto
    {
        private readonly IServiceProduto _serviceProduto;
        private readonly IMapper _mapper;

        public ApplicationServiceProduto(IServiceProduto serviceProduto
                                        , IMapper mapper)
        {
            _serviceProduto = serviceProduto;
            _mapper = mapper;
        }

        public void Add(ProdutoDto produtoDto)
        {
            var produto = _mapper.Map<Produto>(produtoDto);
            _serviceProduto.Add(produto);
        }

        public async Task<IEnumerable<ProdutoDto>> GetAllAsync()
        {
            var produtos = await _serviceProduto.GetAllAsync();
            var produtosDto = _mapper.Map<IEnumerable<ProdutoDto>>(produtos);

            return produtosDto;
        }

        public async Task<ProdutoDto> GetByIdAsync(int id)
        {
            var produto = await _serviceProduto.GetByIdAsync(id);
            var produtoDto = _mapper.Map<ProdutoDto>(produto);

            return produtoDto;
        }

        public bool Remove(int id)
        {
            try
            {
                var produto = _serviceProduto.GetByIdAsync(id);

                if (produto == null)
                    return false;

                _serviceProduto.Remove(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Update(ProdutoDto produtoDto)
        {
            var produto = _mapper.Map<Produto>(produtoDto);
            _serviceProduto.Update(produto);
        }
    }
}