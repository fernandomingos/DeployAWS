using DeployAWS.Domain.Core.Interfaces.Repositorys;
using DeployAWS.Domain.Core.Interfaces.Services;
using DeployAWS.Domain.Entitys;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeployAWS.Domain.Services
{
    public class ServiceBaseNoSQL<TEntity> : IServiceBaseNoSQL<TEntity> where TEntity : class
    {
        private readonly IRepositoryBaseNoSQL<TEntity> _repository;

        public ServiceBaseNoSQL(IRepositoryBaseNoSQL<TEntity> repository) =>
            _repository = repository;

        public void CreateAsync(Product product) =>
            _repository.CreateAsync(product);

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Product> GetByIdAsync(string id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public bool Remove(string id)
        {
           return _repository.Remove(id);
        }

        public void Update(Product product) =>
            _repository.Update(product);
    }
}
