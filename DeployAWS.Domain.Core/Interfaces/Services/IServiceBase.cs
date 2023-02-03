using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeployAWS.Domain.Core.Interfaces.Services
{
    public interface IServiceBase<TEntity> where TEntity : class
    {
        void Add(TEntity obj);

        void Update(TEntity obj);

        void Remove(int id);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> GetAsync(TEntity obj);
    }
}