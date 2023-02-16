using DeployAWS.Domain.Core.Interfaces.Repositorys;
using DeployAWS.Domain.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeployAWS.Domain.Services
{
    public class ServiceBase<TEntity> : IServiceBase<TEntity> where TEntity : class
    {
        private readonly IRepositoryBase<TEntity> _repository;

        public ServiceBase(IRepositoryBase<TEntity> repository)
        {
            _repository = repository;
        }

        public void Add(TEntity obj)
        {
            _repository.Add(obj);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<TEntity> GetAsync(TEntity obj)
        {
            return await _repository.GetAsync(obj);
        }

        public async Task<TEntity> GetByIdAsync(string id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public void Remove(string id)
        {
            var obj = _repository.GetByIdAsync(id);
            _repository.Remove(obj.Result);
        }

        public void Update(TEntity obj)
        {
            _repository.Update(obj);
        }
    }
}