using AutoMapper;
using DeployAWS.Application.Dtos;
using DeployAWS.Application.Interfaces;
using DeployAWS.Domain.Core.Interfaces.Services;
using DeployAWS.Domain.Entitys;
using System;
using System.Collections.Generic;

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

        public IEnumerable<ProdutoDto> GetAll()
        {
            var produtos = _serviceProduto.GetAll();
            var produtosDto = _mapper.Map<IEnumerable<ProdutoDto>>(produtos);
            return produtosDto;
        }

        public ProdutoDto GetById(int id)
        {
            var produto = _serviceProduto.GetById(id);
            var produtoDto = _mapper.Map<ProdutoDto>(produto);

            return produtoDto;
        }

        public bool Remove(int id)
        {
            try
            {
                var produto = _serviceProduto.GetById(id);

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