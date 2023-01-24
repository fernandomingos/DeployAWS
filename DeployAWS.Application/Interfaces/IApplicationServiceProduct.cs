using DeployAWS.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeployAWS.Application.Interfaces
{
    public interface IApplicationServiceProduct
    {
        void Add(ProductDto productDto);

        void Update(ProductDto productDto);

        bool Remove(int id);

        Task<IEnumerable<ProductDto>> GetAllAsync();

        Task<ProductDto> GetByIdAsync(int id);
    }
}