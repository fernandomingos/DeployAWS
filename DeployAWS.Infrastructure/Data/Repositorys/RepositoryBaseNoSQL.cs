using DeployAWS.Domain.Core.Interfaces.Repositorys;
using DeployAWS.Domain.Entitys;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeployAWS.Infrastructure.Data.Repositorys
{
    public class RepositoryBaseNoSQL : IRepositoryBaseNoSQL<Product>
    {
        private readonly AppDbNoSQLContext _appDbNoSQLContext;

        public RepositoryBaseNoSQL(AppDbNoSQLContext appDbNoSQLContext) =>
            _appDbNoSQLContext = appDbNoSQLContext;

        public void CreateAsync(Product product) =>
            _appDbNoSQLContext.CreateAsync(product).ConfigureAwait(false);

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _appDbNoSQLContext.GetAllAsync();
        }

        public async Task<Product> GetByIdAsync(string id)
        {
            return await _appDbNoSQLContext.GetAsyncById(id);
        }

        public void RemoveAsync(string id) =>
            _appDbNoSQLContext.RemoveAsync(id).ConfigureAwait(false);

        public void UpdateAsync(Product product) =>
            _appDbNoSQLContext.UpdateAsync(product).ConfigureAwait(false);
    }
}
