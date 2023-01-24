using DeployAWS.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeployAWS.Application.Interfaces
{
    public interface IApplicationServiceProduto
    {
        void Add(ProdutoDto produtoDto);

        void Update(ProdutoDto produtoDto);

        bool Remove(int id);

        Task<IEnumerable<ProdutoDto>> GetAllAsync();

        Task<ProdutoDto> GetByIdAsync(int id);
    }
}