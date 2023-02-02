using DeployAWS.Domain.Entitys;
using DeployAWS.Infrastructure.Settings.NoSQL;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;

namespace DeployAWS.Infrastructure.Data
{
    public class AppDbNoSQLContext
    {
        private readonly IMongoDatabase _mongoDataBase;

        public AppDbNoSQLContext(IOptions<ProductDatabaseSettings> productSettings)
        {
            var mongoClient = new MongoClient(productSettings.Value.ConnectionStrings);

            if (mongoClient == null)
                throw new ArgumentNullException(nameof(mongoClient));

            _mongoDataBase = mongoClient.GetDatabase(productSettings.Value.DataBaseName);
            _mongoDataBase.GetCollection<Product>("Products");
        }

        public IMongoCollection<Product> Products
        {
            get
            {
                return _mongoDataBase.GetCollection<Product>("Products");
            }
        }
    }
}
