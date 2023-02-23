using DeployAWS.Domain.Entitys;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeployAWS.Domain.Core.Interfaces.Repositorys
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        void Add(TEntity obj);
        void Update(TEntity obj);
        void Remove(TEntity obj);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(String id);
        Task<TEntity> GetAsync(TEntity obj);
        Task<TEntity> PostLoginAsync(Login login);
    }
}