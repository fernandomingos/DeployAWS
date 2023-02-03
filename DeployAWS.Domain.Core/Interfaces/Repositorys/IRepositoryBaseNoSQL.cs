using DeployAWS.Domain.Entitys;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeployAWS.Domain.Core.Interfaces.Repositorys
{
    public interface IRepositoryBaseNoSQL<TEntity> where TEntity : class
    {
        void CreateAsync(Product product);

        void Update(Product product);

        bool Remove(string id);

        Task<IEnumerable<Product>> GetAllAsync();

        Task<Product> GetByIdAsync(string id);
    }
}
