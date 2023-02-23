using DeployAWS.Domain.Entitys;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeployAWS.Domain.Core.Interfaces.Services
{
    public interface IServiceBase<TEntity> where TEntity : class
    {
        void Add(TEntity obj);
        void Update(TEntity obj);
        void Remove(string id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(String id);
        Task<TEntity> GetAsync(TEntity obj);
        Task<TEntity> PostLoginAsync(Login login);
    }
}