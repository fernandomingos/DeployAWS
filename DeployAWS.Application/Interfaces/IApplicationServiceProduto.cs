using DeployAWS.Application.Dtos;
using System.Collections.Generic;

namespace DeployAWS.Application.Interfaces
{
    public interface IApplicationServiceProduto
    {
        void Add(ProdutoDto produtoDto);

        void Update(ProdutoDto produtoDto);

        bool Remove(int id);

        IEnumerable<ProdutoDto> GetAll();

        ProdutoDto GetById(int id);
    }
}