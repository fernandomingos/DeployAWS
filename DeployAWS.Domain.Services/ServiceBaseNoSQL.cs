using DeployAWS.Domain.Core.Interfaces.Repositorys;
using DeployAWS.Domain.Core.Interfaces.Services;
using DeployAWS.Domain.Entitys;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeployAWS.Domain.Services
{
    public class ServiceBaseNoSQL : IServiceBaseNoSQL<Product>
    {
        private readonly IRepositoryBaseNoSQL<Product> _repository;

        public ServiceBaseNoSQL(IRepositoryBaseNoSQL<Product> repository) =>
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

        public void RemoveAsync(string id) =>
            _repository.RemoveAsync(id);

        public void UpdateAsync(Product product) =>
            _repository.UpdateAsync(product);
    }
}
