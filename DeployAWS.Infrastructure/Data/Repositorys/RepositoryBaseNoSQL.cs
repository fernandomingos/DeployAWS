using DeployAWS.Domain.Core.Interfaces.Repositorys;
using DeployAWS.Domain.Entitys;
using DeployAWS.Infrastructure.Settings.NoSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeployAWS.Infrastructure.Data.Repositorys
{
    public class RepositoryBaseNoSQL<TEntity> : IRepositoryBaseNoSQL<TEntity> where TEntity : class
    {
        private readonly AppDbNoSQLContext _appDbNoSQLContext;

        public RepositoryBaseNoSQL(IOptions<ProductDatabaseSettings> options) =>
            _appDbNoSQLContext = new AppDbNoSQLContext(options);

        public void CreateAsync(Product product) =>
            _appDbNoSQLContext.Products.InsertOneAsync(product);

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _appDbNoSQLContext.Products.Find(x => true).ToListAsync();
        }

        public async Task<Product> GetByIdAsync(string id)
        {
            return await _appDbNoSQLContext.Products.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public bool Remove(string id)
        { 
            var result = _appDbNoSQLContext.Products.DeleteOne(p => p.Id == id);
            return result.DeletedCount > 0;
        }

        public void Update(Product product) 
        {
            var filter = Builders<Product>.Filter.Eq("Id", product.Id);
            _appDbNoSQLContext.Products.ReplaceOne(filter, product);
        }
    }
}
