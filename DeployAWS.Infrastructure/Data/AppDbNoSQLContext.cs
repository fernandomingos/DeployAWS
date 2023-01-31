using DeployAWS.Domain.Entitys;
using DeployAWS.Infrastructure.Settings.NoSQL;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeployAWS.Infrastructure.Data
{
    public class AppDbNoSQLContext
    {
        private readonly IMongoCollection<Product> _productColletion;

        public AppDbNoSQLContext(IOptions<ProductDatabaseSettings> productSettings)
        {
            var mongoClient = new MongoClient(productSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(productSettings.Value.DataBaseName);

            _productColletion = mongoDatabase.GetCollection<Product> (productSettings.Value.ProductCollectionName);
        }

        public async Task<IEnumerable<Product>> GetAllAsync() =>
            await _productColletion.Find(x => true).ToListAsync();

        public async Task<Product> GetAsyncById(string id) => 
            await _productColletion.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Product product) =>
            await _productColletion.InsertOneAsync(product);

        public async Task UpdateAsync(Product product) =>
            await _productColletion.ReplaceOneAsync(x => x.Id == product.Id, product);

        public async Task RemoveAsync(string id) =>
            await _productColletion.DeleteOneAsync(x => x.Id == id);
    }
}
