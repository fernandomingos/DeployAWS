﻿using DeployAWS.Application.Dtos;
using System.Collections.Generic;

namespace DeployAWS.Application.Interfaces
{
    public interface IApplicationServiceProduto
    {
        void Add(ProdutoDto produtoDto);

        void Update(ProdutoDto produtoDto);

        void Remove(ProdutoDto produtoDto);

        IEnumerable<ProdutoDto> GetAll();

        ProdutoDto GetById(int id);
    }
}