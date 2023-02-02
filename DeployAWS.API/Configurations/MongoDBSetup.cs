using DeployAWS.Infrastructure.Settings.NoSQL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DeployAWS.API.Configurations
{
    public static class MongoDBSetup
    {
        public static void AddMongoDBSetup(this IServiceCollection services, IConfiguration configuration, bool IsDockerConnection = true)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (IsDockerConnection)
            {
                services.Configure<ProductDatabaseSettings>(options =>
                {
                    options.ConnectionStrings = configuration.GetSection("NoSQLConnStringsDocker:ConnectionStrings").Value;
                    options.DataBaseName = configuration.GetSection("NoSQLConnStringsDocker:DataBaseName").Value;
                    options.CollectionName = configuration.GetSection("NoSQLConnStringsDocker:CollectionName").Value;
                });
            }
            else
            {
                services.Configure<ProductDatabaseSettings>(options =>
                {

                    options.ConnectionStrings = configuration.GetSection("NoSQLConnStringsOnline:ConnectionStrings").Value;
                    options.DataBaseName = configuration.GetSection("NoSQLConnStringsOnline:DataBaseName").Value;
                    options.CollectionName = configuration.GetSection("NoSQLConnStringsOnline:CollectionName").Value;
                });
            }
        }
    }
}
