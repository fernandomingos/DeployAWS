using DeployAWS.Infrastructure.Settings.NoSQL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DeployAWS.API.Configurations
{
    public static class MongoDBSetup
    {
        public static void AddMongoDBSetup(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.Configure<ProductDatabaseSettings>(options =>
            {
                options.ConnectionString = configuration.GetSection("NoSQLConnStrings:ConnectionStrings").Value;
                options.DataBaseName = configuration.GetSection("NoSQLConnStrings:DataBaseName").Value;
                options.CollectionName = configuration.GetSection("NoSQLConnStrings:CollectionName").Value;
            });
        }
    }
}
