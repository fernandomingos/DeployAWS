using DeployAWS.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeployAWS.Application.Interfaces
{
    public interface IApplicationServiceProduct
    {
        void CreateAsync(ProductDto productDto);

        void UpdateAsync(ProductDto productDto);

        bool RemoveAsync(string id);

        Task<IEnumerable<ProductDto>> GetAllAsync();

        Task<ProductDto> GetByIdAsync(string id);
    }
}