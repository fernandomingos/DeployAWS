using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeployAWS.Domain.Core.Interfaces.Services
{
    public interface IServiceBaseNoSQL<Product>
    {
        void CreateAsync(Product product);

        void UpdateAsync(Product product);

        void RemoveAsync(string id);

        Task<IEnumerable<Product>> GetAllAsync();

        Task<Product> GetByIdAsync(string id);
    }
}
