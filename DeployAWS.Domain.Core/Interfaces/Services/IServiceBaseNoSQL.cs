using DeployAWS.Domain.Entitys;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeployAWS.Domain.Core.Interfaces.Services
{
    public interface IServiceBaseNoSQL<TEntity> where TEntity : class
    {
        void CreateAsync(Product product);

        void Update(Product product);

        void Remove(string id);

        Task<IEnumerable<Product>> GetAllAsync();

        Task<Product> GetByIdAsync(string id);
    }
}
